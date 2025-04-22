using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.Services.Interfaces;
using ServiceManagement.DTOs;
using ServiceManagement.Models;
using ServiceManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Services
{
    public class IntentionServiceService : IIntentionServiceService
    {
        private readonly IIntentionServiceRepository _repository;
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IValidationService _validationService;

        public IntentionServiceService(
            IIntentionServiceRepository repository,
            IServiceTypeRepository serviceTypeRepository,
            IValidationService validationService)
        {
            _repository = repository;
            _serviceTypeRepository = serviceTypeRepository;
            _validationService = validationService;
        }

        public async Task<List<IntentionServiceDto>> GetAllIntentionServicesAsync()
        {
            var intentionServices = await _repository.GetAllAsync();
            return intentionServices.Select(i => new IntentionServiceDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                ServiceTypeId = i.ServiceTypeId,
                ServiceTypeName = i.ServiceType?.Name,
                Limit_Area = i.Limit_Area,
                Daily_Price = i.Daily_Price,
                UrbanConfrontation = i.UrbanConfrontation,
                RuralConfrontation = i.RuralConfrontation
            }).ToList();
        }

        public async Task<IntentionServiceDto> GetIntentionServiceByIdAsync(int id)
        {
            var intentionService = await _repository.GetByIdAsync(id);
            if (intentionService == null) return null;

            return new IntentionServiceDto
            {
                Id = intentionService.Id,
                Name = intentionService.Name,
                Description = intentionService.Description,
                ServiceTypeId = intentionService.ServiceTypeId,
                ServiceTypeName = intentionService.ServiceType?.Name,
                Limit_Area = intentionService.Limit_Area,
                Daily_Price = intentionService.Daily_Price,
                UrbanConfrontation = intentionService.UrbanConfrontation,
                RuralConfrontation = intentionService.RuralConfrontation
            };
        }

        public async Task<IntentionServiceDto> CreateIntentionServiceAsync(IntentionServiceDto intentionServiceDto)
        {
            // Validate the intention service
            _validationService.ValidateIntentionService(intentionServiceDto);

            // Verify if the ServiceType exists
            var serviceType = await _serviceTypeRepository.GetByIdAsync(intentionServiceDto.ServiceTypeId);
            if (serviceType == null)
                throw new Exception($"ServiceType with ID {intentionServiceDto.ServiceTypeId} not found");

            var intentionService = new IntentionServiceDbo
            {
                Name = intentionServiceDto.Name,
                Description = intentionServiceDto.Description,
                ServiceTypeId = intentionServiceDto.ServiceTypeId,
                Limit_Area = intentionServiceDto.Limit_Area,
                Daily_Price = intentionServiceDto.Daily_Price,
                UrbanConfrontation = intentionServiceDto.UrbanConfrontation,
                RuralConfrontation = intentionServiceDto.RuralConfrontation,
                IsActive = true
            };

            var createdIntentionService = await _repository.CreateAsync(intentionService);
            return new IntentionServiceDto
            {
                Id = createdIntentionService.Id,
                Name = createdIntentionService.Name,
                Description = createdIntentionService.Description,
                ServiceTypeId = createdIntentionService.ServiceTypeId,
                ServiceTypeName = serviceType.Name,
                Limit_Area = createdIntentionService.Limit_Area,
                Daily_Price = createdIntentionService.Daily_Price,
                UrbanConfrontation = createdIntentionService.UrbanConfrontation,
                RuralConfrontation = createdIntentionService.RuralConfrontation
            };
        }

        public async Task UpdateIntentionServiceAsync(int id, IntentionServiceDto intentionServiceDto)
        {
            // Validate the intention service
            _validationService.ValidateIntentionService(intentionServiceDto);

            var existingIntentionService = await _repository.GetByIdAsync(id);
            if (existingIntentionService == null)
                throw new Exception($"IntentionService with ID {id} not found");

            // Check if ServiceType exists if it's being changed
            if (intentionServiceDto.ServiceTypeId != existingIntentionService.ServiceTypeId)
            {
                var serviceType = await _serviceTypeRepository.GetByIdAsync(intentionServiceDto.ServiceTypeId);
                if (serviceType == null)
                    throw new Exception($"ServiceType with ID {intentionServiceDto.ServiceTypeId} not found");
            }

            existingIntentionService.Name = intentionServiceDto.Name;
            existingIntentionService.Description = intentionServiceDto.Description;
            existingIntentionService.ServiceTypeId = intentionServiceDto.ServiceTypeId;
            existingIntentionService.Limit_Area = intentionServiceDto.Limit_Area;
            existingIntentionService.Daily_Price = intentionServiceDto.Daily_Price;
            existingIntentionService.UrbanConfrontation = intentionServiceDto.UrbanConfrontation;
            existingIntentionService.RuralConfrontation = intentionServiceDto.RuralConfrontation;

            await _repository.UpdateAsync(existingIntentionService);
        }

        public async Task DeleteIntentionServiceAsync(int id)
        {
            var existingIntentionService = await _repository.GetByIdAsync(id);
            if (existingIntentionService == null)
                throw new KeyNotFoundException($"IntentionService with ID {id} not found");

            // Check if the intention service is associated with any budget
            bool isAssociated = await _repository.IsAssociatedWithBudgetAsync(id);

            if (isAssociated)
            {
                // Deactivate instead of delete if associated with budget
                await _repository.DeactivateAsync(id);
            }
            else
            {
                // Delete if not associated with any budget
                await _repository.DeleteAsync(id);
            }
        }

        public async Task<List<IntentionServiceDto>> GetIntentionServicesByServiceTypeIdAsync(int serviceTypeId)
        {
            var intentionServices = await _repository.GetByServiceTypeIdAsync(serviceTypeId);
            return intentionServices.Select(i => new IntentionServiceDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                ServiceTypeId = i.ServiceTypeId,
                ServiceTypeName = i.ServiceType?.Name,
                Limit_Area = i.Limit_Area,
                Daily_Price = i.Daily_Price,
                UrbanConfrontation = i.UrbanConfrontation,
                RuralConfrontation = i.RuralConfrontation
            }).ToList();
        }
    }
}