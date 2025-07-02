using GeoSolucoesAPI.Custom;
using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.DTOs.BudgetResponse;
using GeoSolucoesAPI.DTOs.Request;
using GeoSolucoesAPI.DTOs.Update;
using GeoSolucoesAPI.Helpers;
using GeoSolucoesAPI.Models;
using GeoSolucoesAPI.Repositories;
using GeoSolucoesAPI.Services.Interfaces;
using GeoSolucoesAPI.Services.Validations;
using Google.Api.Gax.Grpc;
using Google.Apis.Auth.OAuth2;
using Google.Maps.Routing.V2;
using Google.Type;
using ServiceManagement.DTOs;
using ServiceManagement.Models;
using ServiceManagement.Repositories;
using System.Globalization;
using System.Text.Json;

namespace GeoSolucoesAPI.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IServiceTypeRepository _ServiceTypeRepository;
        private readonly IGeoLocationService _geoLocationService;
        private readonly IValidationService _validationService;
        private readonly IEmailService _emailService;
        public BudgetService(IBudgetRepository budgetRepository
            , IServiceTypeRepository serviceTypeRepository
            , IGeoLocationService geoLocationService
            , IValidationService validationService
            , IEmailService emailService)
        {
            _budgetRepository = budgetRepository;
            _ServiceTypeRepository = serviceTypeRepository;
            _geoLocationService = geoLocationService;
            _validationService = validationService;
            _emailService = emailService;
        }

        public async Task<BudgetDto> PostBudget(BudgetRequest budget)
        {
            try
            {
                _validationService.BudgetValidation(budget);

                var validationErrors = new List<ValidationError>();
                void AddError(string field, string message)
                {
                    validationErrors.Add(new ValidationError(field, message));
                }

                ValidationException GetError()
                    => new("Erro durante orçamento", validationErrors);

                var serviceType = await _ServiceTypeRepository.GetByIdAsync(budget.ServiceTypeId);

                if (serviceType is null)
                {
                    AddError("serviceType", $"Tipo de serviço não encontrado, {FunctionHelpers.ContactAdm()}");
                    throw GetError();
                }

                var intentionService = serviceType.IntentionServices.FirstOrDefault(x => x.Id == budget.IntentionServiceId);

                if (intentionService is null)
                {
                    AddError("intentionService", $"A intenção de serviço não encontrada, ou não faz parte do tipo de serviço, {FunctionHelpers.ContactAdm()}");
                    throw GetError();
                }

                var budgetDbo = new BudgetDbo()
                {
                    Price = budget.Price,
                    StartDate = budget.StartDate.ToUniversalTime(),
                    EndDate = budget.EndDate.ToUniversalTime(),
                    UserId = budget.UserId,
                    AreaSize = budget.BudgetAreaSettings.Area_Size,
                    UnitOfMeasure = budget.BudgetAreaSettings.UnitOfMeasure,
                    ServiceTypeId = serviceType.Id,
                    IntentionServiceId = intentionService.Id,
                    Confrontations = budget.Confrontations,
                    Address = new AddressDbo
                    {
                        Neighborhood = budget.Address.Neighborhood,
                        Number = budget.Address.Number,
                        City = budget.Address.City,
                        Complement = budget.Address.Complement,
                        Street = budget.Address.Street,
                        Zipcode = budget.Address.Zipcode,
                        State = budget.Address.State
                    },
                };

                var budgetCreated = await _budgetRepository.PostBudget(budgetDbo);
                var budgetCreatedDto = this.ConvertBudgetDboToDto(budgetCreated);

                // Gerar PDF e enviar por e-mail
                var pdfBytes = PdfHelper.GenerateBudgetPdf(budgetCreatedDto);
                                await _emailService.SendEmailWithAttachmentAsync(
                    budgetCreatedDto.User.Email,
                    "Orçamento gerado",
                    "<p>Segue em anexo o orçamento solicitado.</p>",
                    pdfBytes,
                    "orcamento.pdf"
                );

                return budgetCreatedDto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<CalcResponse> ProcessCalc(CalcParameters calcParameters)
        {
            //First simple validation
            _validationService.CalcValidation(calcParameters);

            var validationErrors = new List<ValidationError>();
            void AddError(string field, string message)
            {
                validationErrors.Add(new ValidationError(field, message));
            }

            ValidationException GetError()
                => new("Erro durante calculo de orçamento", validationErrors);



            //Convert areaSize to meters
            var finalPrice = new decimal();

            //Deslocamento
            var distanceRange = await _budgetRepository.GetDistance();
            //Ponto de partida
            var startPoint = await _budgetRepository.GetStartPoint();
            var confrontation = await _budgetRepository.GetConfrontations();
            var hosting = await _budgetRepository.GetHosting();
            var serviceType = await _ServiceTypeRepository.GetByIdAsync(calcParameters.ServiceTypeId);

            if (startPoint is null)
            {
                AddError("DistanceRange", $"Ponto de partida não encontrado, {FunctionHelpers.ContactAdm()}");
                throw GetError();
            }

            if (serviceType is null)
            {
                AddError("ServiceType", $"O tipo de serviço não foi encontrado, {FunctionHelpers.ContactAdm()}");
                throw GetError();
            }

            var intentionService = serviceType.IntentionServices.FirstOrDefault(x => x.Id == calcParameters.IntentionServiceId);
            if (intentionService is null)
            {
                AddError("IntentionService", $"A intenção de serviço não faz parte do Serviço principal, {FunctionHelpers.ContactAdm()}");
                throw GetError();
            }


            //Estipular tipo/ m² ou Hectar
            var areaSize = calcParameters.AreaSettings.UnitOfMeasure == EUnitOfMeasure.Hectare
                                ? calcParameters.AreaSettings.Area_Size.ConvertHectarToSquareMeter()
                                : calcParameters.AreaSettings.Area_Size;

            var height = (int)Math.Ceiling(areaSize / intentionService.Limit_Area);

            //Peso e preço inicial definido
            finalPrice = intentionService.Daily_Price * height;

            //Distancia final
            var finalDistance = await _geoLocationService.GetDistanceFromStartEndPoint(startPoint, calcParameters.Address);



            //Hospedagem
            if (height - 1 > 0)
            {
                float finalDistanceFloat = (float)finalDistance;
                var actualHosting = hosting.FirstOrDefault(x => x.DistanteMin.ConvertToMeterFloat() <= finalDistanceFloat && finalDistanceFloat <= x.DistanteMax.ConvertToMeterFloat());
                if (actualHosting is not null)
                {
                    finalPrice += actualHosting.Price > 0 ? (actualHosting.Price * (height - 1)) : finalPrice;
                }


            }
            //TODO: Ajustar deslocamento(nao usar termo area)

            if (distanceRange.IsNotNullAndAny())
            {
                var multiplierDistance = distanceRange.FirstOrDefault(x => x.AreaMin.ConvertToMeter() <= finalDistance && finalDistance <= x.AreaMax.ConvertToMeter());

                if (multiplierDistance is not null)
                {
                    finalPrice += (multiplierDistance.Multiplier * finalDistance.ConvertToQuilometer() * 2);
                }

            }

            //Confrontação
            if (calcParameters.Confrontations > 0 && (intentionService.UrbanConfrontation || intentionService.RuralConfrontation))
            {
                if (confrontation.IsNotNullAndAny())
                {

                    var confrontationSelected = confrontation.FirstOrDefault(x => x.AreaMin.ConvertToMeter() <= finalDistance && finalDistance <= x.AreaMax.ConvertToMeter()
                                                                          && x.UrbanConfrontation == intentionService.UrbanConfrontation
                                                                          && x.RuralConfrontation == intentionService.RuralConfrontation);

                    if (confrontationSelected is not null)
                    {
                        finalPrice += (confrontationSelected.Price * calcParameters.Confrontations);
                    }
                }
            }
            return new CalcResponse() { CalcParametersResponse = finalPrice.ToString("C", new CultureInfo("pt-BR")) };

        }


        public async Task<RemovedItem> DeleteBudget(int budgetId)
        {
            var validationErrors = new List<ValidationError>();
            void AddError(string field, string message)
            {
                validationErrors.Add(new ValidationError(field, message));
            }

            ValidationException GetError()
                => new("Erro durante remoção do orçamento", validationErrors);

            if (budgetId == 0)
            {
                AddError("Orçamento", $"Selecione um orçamento válido");
                throw GetError();
            }
            var budgetToDelete = await _budgetRepository.GetBudgetById(budgetId);
            if (budgetToDelete is null)
            {
                AddError("Orçamento", $"O orçamento selecionado não foi encontrado na base de dados");
                throw GetError();
            }
            await _budgetRepository.DeleteBudget(budgetToDelete);

            return new RemovedItem()
            {
                Success = true,
                Message = "Orçamento removido com sucesso"
            };
        }

        public async Task<List<BudgetDto>> GetAllBudgets()
        {
            try
            {
                var allBudgets = await _budgetRepository.GetAllBudgets();
                return allBudgets.Select(x => this.ConvertBudgetDboToDto(x)).ToList();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private BudgetDto ConvertBudgetDboToDto(BudgetDbo budgetDbo)
        {
            var budgetDto = new BudgetDto()
            {
                Id = budgetDbo.Id,
                Price = budgetDbo.Price,
                StartDate = budgetDbo.StartDate,
                EndDate = budgetDbo.EndDate,
                Confrontations = budgetDbo.Confrontations,
                BudgetAreaSettings = new AreaParameters
                {
                    Area_Size = budgetDbo.AreaSize,
                    UnitOfMeasure = budgetDbo.UnitOfMeasure
                },
                User = new BudgetUser
                {
                    Id = budgetDbo.User.Id,
                    Email = budgetDbo.User.Email,
                    Cell = budgetDbo.User.Cell,
                    UserType = budgetDbo.User.UserType
                },
                Address = new AddressResponse()
                {
                    Id = budgetDbo.Address.Id,
                    Zipcode = budgetDbo.Address.Zipcode,
                    Neighborhood = budgetDbo.Address.Neighborhood,
                    Street = budgetDbo.Address.Street,
                    City = budgetDbo.Address.City,
                    State = budgetDbo.Address.State,
                    Number = budgetDbo.Address.Number,
                    Complement = budgetDbo.Address.Complement
                },
                ServiceType = new ServiceTypeResponse()
                {
                    Id = budgetDbo.ServiceType.Id,
                    Name = budgetDbo.ServiceType.Name,
                    Description = budgetDbo.ServiceType.Description
                },
                IntentionService = new IntentionServiceResponse()
                {
                    Id = budgetDbo.IntentionService.Id,
                    Daily_Price = budgetDbo.IntentionService.Daily_Price,
                    UrbanConfrontation = budgetDbo.IntentionService.UrbanConfrontation,
                    RuralConfrontation = budgetDbo.IntentionService.RuralConfrontation,
                    Name = budgetDbo.IntentionService.Name,
                    Description = budgetDbo.IntentionService.Description,
                    Limit_Area = budgetDbo.IntentionService.Limit_Area
                }

            };
            return budgetDto;
        }
        private BudgetDbo ConvertBudgetDtoToDbo(BudgetDto bydgetDto)
        {
            var budgetDbo = new BudgetDbo()
            {
                Price = bydgetDto.Price,
                StartDate = bydgetDto.StartDate,
                EndDate = bydgetDto.EndDate,
                UserId = bydgetDto.User.Id,
                AreaSize = bydgetDto.BudgetAreaSettings.Area_Size,
                UnitOfMeasure = bydgetDto.BudgetAreaSettings.UnitOfMeasure,
                ServiceTypeId = bydgetDto.ServiceType.Id,
                IntentionServiceId = bydgetDto.IntentionService.Id,
                Confrontations = bydgetDto.Confrontations,
                Address = new AddressDbo
                {
                    Neighborhood = bydgetDto.Address.Neighborhood,
                    Number = bydgetDto.Address.Number,
                    City = bydgetDto.Address.City,
                    Complement = bydgetDto.Address.Complement,
                    Street = bydgetDto.Address.Street,
                    Zipcode = bydgetDto.Address.Zipcode,
                    State = bydgetDto.Address.State
                },
            };

            return budgetDbo;
        }

        private BudgetDbo ConvertBudgetUpdateToDbo(int budgetId, BudgetUpdate budgetUpdate)
        {
            var budgetDbo = new BudgetDbo()
            {
                Id = budgetId,
                Price = budgetUpdate.Price,
                StartDate = budgetUpdate.StartDate,
                EndDate = budgetUpdate.EndDate,
                UserId = budgetUpdate.UserId,
                AreaSize = budgetUpdate.BudgetAreaSettings.Area_Size,
                UnitOfMeasure = budgetUpdate.BudgetAreaSettings.UnitOfMeasure,
                ServiceTypeId = budgetUpdate.ServiceTypeId,
                IntentionServiceId = budgetUpdate.IntentionServiceId,
                Confrontations = budgetUpdate.Confrontations,
                Address = new AddressDbo
                {
                    Neighborhood = budgetUpdate.Address.Neighborhood,
                    Number = budgetUpdate.Address.Number,
                    City = budgetUpdate.Address.City,
                    Complement = budgetUpdate.Address.Complement,
                    Street = budgetUpdate.Address.Street,
                    Zipcode = budgetUpdate.Address.Zipcode,
                    State = budgetUpdate.Address.State
                },
            };

            return budgetDbo;
        }

        public async Task<BudgetDto> UpdateBudget(int budgetId, BudgetUpdate budget)
        {

            try
            {
                var validationErrors = new List<ValidationError>();
                void AddError(string field, string message)
                {
                    validationErrors.Add(new ValidationError(field, message));
                }

                ValidationException GetError()
                    => new(ErrorMessageHelpers.BudgetUpdateError, validationErrors);


                var oldBudgetDb = await _budgetRepository.GetBudgetById(budgetId);
                if (oldBudgetDb == null)
                {
                    AddError("Orçamento", ErrorMessageHelpers.BudgetNotFound);
                    throw GetError();
                }


                var newBudgetDbo = this.ConvertBudgetUpdateToDbo(budgetId, budget);

                var serviceType = await _ServiceTypeRepository.GetByIdAsync(budget.ServiceTypeId);

                if (serviceType is null)
                {
                    AddError("serviceType", ErrorMessageHelpers.ServiceNotFound);
                    throw GetError();

                }
                var intentionService = serviceType.IntentionServices.FirstOrDefault(x => x.Id == budget.IntentionServiceId);


                if (intentionService is null)
                {
                    AddError("intentionService", ErrorMessageHelpers.IntentionServiceNotFound);
                    throw GetError();
                }


                oldBudgetDb.UserId = newBudgetDbo.UserId;
                oldBudgetDb.Price = newBudgetDbo.Price;
                oldBudgetDb.StartDate = newBudgetDbo.StartDate;
                oldBudgetDb.EndDate = newBudgetDbo.EndDate;
                oldBudgetDb.AreaSize = newBudgetDbo.AreaSize;
                oldBudgetDb.UnitOfMeasure = newBudgetDbo.UnitOfMeasure;
                oldBudgetDb.Confrontations = newBudgetDbo.Confrontations;
                oldBudgetDb.IntentionServiceId = newBudgetDbo.IntentionServiceId;
                oldBudgetDb.ServiceTypeId = newBudgetDbo.ServiceTypeId;

                if (oldBudgetDb.Address == null)
                    oldBudgetDb.Address = new AddressDbo();

                oldBudgetDb.Address.Zipcode = newBudgetDbo.Address.Zipcode;
                oldBudgetDb.Address.Neighborhood = newBudgetDbo.Address.Neighborhood;
                oldBudgetDb.Address.Street = newBudgetDbo.Address.Street;
                oldBudgetDb.Address.City = newBudgetDbo.Address.City;
                oldBudgetDb.Address.State = newBudgetDbo.Address.State;
                oldBudgetDb.Address.Number = newBudgetDbo.Address.Number;
                oldBudgetDb.Address.Complement = newBudgetDbo.Address.Complement;

                var budgetUpdated = await _budgetRepository.UpdateBudget(oldBudgetDb);
                var budgetUpdatedDto = this.ConvertBudgetDboToDto(budgetUpdated);

                return budgetUpdatedDto;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<BudgetDto> GetBudgetById(int budgetId)
        {
            try
            {
                var validationErrors = new List<ValidationError>();
                void AddError(string field, string message)
                {
                    validationErrors.Add(new ValidationError(field, message));
                }

                ValidationException GetError()
                    => new(ErrorMessageHelpers.BudgetUpdateError, validationErrors);

                if (budgetId is 0)
                {
                    AddError("Budget", ErrorMessageHelpers.GetBudgetError);
                    throw GetError();
                }

                var budgetFromDatabase = await _budgetRepository.GetBudgetById(budgetId);


                if (budgetFromDatabase is null)
                {
                    AddError("Budget", ErrorMessageHelpers.BudgetNotFound);
                    throw GetError();
                }

                return this.ConvertBudgetDboToDto(budgetFromDatabase);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task<AddressResponse> GetAddressByCep(string cep)
        {
            var validationErrors = new List<ValidationError>();
            void AddError(string field, string message)
            {
                validationErrors.Add(new ValidationError(field, message));
            }

            ValidationException GetError()
                => new(ErrorMessageHelpers.BudgetUpdateError, validationErrors);

            if (string.IsNullOrWhiteSpace(cep))
            {
                AddError("Cep", "O CEP não pode estar vazio.");
                throw GetError();
            }

            var cleanedCep = cep.Replace("-", "").Trim();

            if (cleanedCep.Length != 8)
            {
                AddError("Cep", "O CEP deve conter exatamente 8 dígitos.");
                throw GetError();
            }

            if (!cleanedCep.All(char.IsDigit))
            {
                AddError("Cep", "O CEP deve conter apenas números.");
                throw GetError();
            }

            var AddressResponse = await _geoLocationService.GetAddressByCep(cleanedCep);

            return AddressResponse;
        }
    }
}
