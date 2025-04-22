using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.DTOs.Request;
using GeoSolucoesAPI.Helpers;
using GeoSolucoesAPI.Services.Interfaces;
using GeoSolucoesAPI.Services.Validations;
using ServiceManagement.DTOs;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GeoSolucoesAPI.Services
{
    public class ValidationService : IValidationService
    {

        public void CalcValidation(CalcParameters calcParameters)
        {
            var validationErrors = new List<ValidationError>();
            void AddError(string field, string message)
            {
                validationErrors.Add(new ValidationError(field, message));
            }

            ValidationException GetError()
                => new("Error during calc validation", validationErrors);

            if (calcParameters == null)
            {
                AddError("Calc Parameter", "Os parãmetros de calculo não podem ser nulos");
            }

            if (calcParameters.Address == null)
            {
                AddError("Address", "O endereço não pode ser nulo");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(calcParameters.Address?.Street))
                    AddError("Street", "Por favor, especifique a rua");
                if (string.IsNullOrWhiteSpace(calcParameters.Address?.Number))
                    AddError("Number", "Por favor, especifiquee o número");
                if (string.IsNullOrWhiteSpace(calcParameters.Address?.Neighborhood))
                    AddError("Neighborhood", "Por favor, especifique o bairro");
                if (string.IsNullOrWhiteSpace(calcParameters.Address?.City))
                    AddError("City", "Por favor, especifique a cidade");
                if (string.IsNullOrWhiteSpace(calcParameters.Address?.State))
                    AddError("State", "Por favor, especifique o estado");
                if (string.IsNullOrWhiteSpace(calcParameters.Address?.Country))
                    AddError("Country", "Por favor, especifique o país");
            }

            if (calcParameters.AreaSettings == null)
            {
                AddError("AreaSettings", "AreaSettings cannot be null");
            }
            else
            {
                if (calcParameters.AreaSettings?.Area_Size <= 0)
                    AddError("Area_Size", "Por favor, especifique o tamanho da área");
            }

            if (calcParameters.ServiceTypeId <= 0)
                AddError("ServiceTypeId", "Por favor, especifique o tipo de serviço");

            if (calcParameters.IntentionServiceId <= 0)
                AddError("IntentionServiceId", "Por favor especifique a intenção de serviço");

            if (validationErrors.Count > 0)
                throw GetError();
        }

        public static List<string> CityValidation(CityDto cityDto)
        {
            var errors = new List<string>();

            if (cityDto == null)
            {
                errors.Add("CityRequest object cannot be null.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(cityDto.Name))
            {
                errors.Add("O nome da cidade é obrigatório.");
            }
            else if (cityDto.Name.Length > 100)
            {
                errors.Add("O nome da cidade deve ter no máximo 100 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(cityDto.State))
            {
                errors.Add("O estado (UF) é obrigatório.");
            }
            else if (cityDto.State.Length != 2)
            {
                errors.Add("O estado (UF) deve ter exatamente 2 caracteres.");
            }

            return errors;
        }

        public void BudgetValidation(BudgetRequest budgetRequest)
        {

            var validationErrors = new List<ValidationError>();
            void AddError(string field, string message)
            {
                validationErrors.Add(new ValidationError(field, message));
            }

            ValidationException GetError()
                => new("Error during budget validation", validationErrors);

            if (budgetRequest == null)
            {
                AddError("Request", "A requisição não pode ser nula.");
                return;
            }

            if (budgetRequest.UserId <= 0)
            {
                AddError("UserId", "Usuário não encontrado.");
            }

            if (budgetRequest.Price <= 0)
            {
                AddError("Price", "Preço incorreto.");
            }

            if (budgetRequest.StartDate == default || budgetRequest.EndDate == default)
            {
                AddError("Dates", "As datas de início e fim devem ser válidas.");
            }
            else if (budgetRequest.EndDate < budgetRequest.StartDate)
            {
                AddError("Dates", "A data de término não pode ser anterior à data de início.");
            }

            if (budgetRequest.BudgetAreaSettings == null)
            {
                AddError("BudgetAreaSettings", "As configurações da área não podem ser nulas.");
            }
            else
            {
                if (budgetRequest.BudgetAreaSettings.Area_Size <= 0)
                {
                    AddError("Area_Size", "O tamanho da área deve ser maior que zero.");
                }

                if (!Enum.IsDefined(typeof(EUnitOfMeasure), budgetRequest.BudgetAreaSettings.UnitOfMeasure))
                {
                    AddError("UnitOfMeasure", "A unidade de medida fornecida é inválida.");
                }
            }

            if (budgetRequest.ServiceTypeId <= 0)
            {
                AddError("ServiceTypeId", "O tipo de serviço deve ser informado.");
            }

            if (budgetRequest.IntentionServiceId <= 0)
            {
                AddError("IntentionServiceId", "A intenção do serviço deve ser informada.");
            }

            if (budgetRequest.Address == null)
            {
                AddError("Address", "O endereço deve ser informado.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(budgetRequest.Address.Zipcode))
                    AddError("Zipcode", "O CEP deve ser informado.");

                if (string.IsNullOrWhiteSpace(budgetRequest.Address.Neighborhood))
                    AddError("Neighborhood", "O bairro deve ser informado.");

                if (string.IsNullOrWhiteSpace(budgetRequest.Address.Street))
                    AddError("Street", "A rua deve ser informada.");

                if (string.IsNullOrWhiteSpace(budgetRequest.Address.City))
                    AddError("City", "A cidade deve ser informada.");

                if (string.IsNullOrWhiteSpace(budgetRequest.Address.State))
                    AddError("State", "O estado deve ser informado.");

                if (budgetRequest.Address.Number == null || budgetRequest.Address.Number <= 0)
                    AddError("Number", "O número do endereço deve ser informado e maior que zero.");
            }

            if (validationErrors.Count > 0)
                throw GetError();

        }

        public void ValidateServiceType(ServiceTypeRequest serviceTypeRequest)
        {
            var validationErrors = new List<ValidationError>();
            void AddError(string field, string message)
            {
                validationErrors.Add(new ValidationError(field, message));
            }

            ValidationException GetError()
                => new("Error during service type validation", validationErrors);

            if (serviceTypeRequest == null)
            {
                AddError("ServiceType", "O tipo de serviço não pode ser nulo");
                throw GetError();
            }

            if (string.IsNullOrWhiteSpace(serviceTypeRequest.Name))
                AddError("Name", "O nome do tipo de serviço é obrigatório");

            if (string.IsNullOrWhiteSpace(serviceTypeRequest.Description))
                AddError("Description", "A descrição do tipo de serviço é obrigatória");

            if (validationErrors.Count > 0)
                throw GetError();
        }

        public void ValidateIntentionService(IntentionServiceDto intentionServiceDto)
        {
            var validationErrors = new List<ValidationError>();
            void AddError(string field, string message)
            {
                validationErrors.Add(new ValidationError(field, message));
            }

            ValidationException GetError()
                => new("Error during intention service validation", validationErrors);

            if (intentionServiceDto == null)
            {
                AddError("IntentionService", "O intuito de serviço não pode ser nulo");
                throw GetError();
            }

            if (string.IsNullOrWhiteSpace(intentionServiceDto.Name))
                AddError("Name", "O nome do intuito de serviço é obrigatório");

            if (string.IsNullOrWhiteSpace(intentionServiceDto.Description))
                AddError("Description", "A descrição do intuito de serviço é obrigatória");

            if (intentionServiceDto.ServiceTypeId <= 0)
                AddError("ServiceTypeId", "O tipo de serviço deve ser especificado");

            if (intentionServiceDto.Limit_Area <= 0)
                AddError("Limit_Area", "O limite de área deve ser maior que zero");

            if (intentionServiceDto.Daily_Price <= 0)
                AddError("Daily_Price", "O preço diário deve ser maior que zero");

            // Validate that only one or none of the confrontation options is selected
            if (intentionServiceDto.UrbanConfrontation && intentionServiceDto.RuralConfrontation)
            {
                AddError("Confrontation", "Apenas uma opção de confrontação pode ser selecionada: Urbana ou Rural");
            }

            if (validationErrors.Count > 0)
                throw GetError();
        }
    }
    }

