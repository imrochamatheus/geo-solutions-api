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
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IServiceTypeRepository _repository;
        private readonly IValidationService _validationService;

        public ServiceTypeService(
            IServiceTypeRepository repository,
            IValidationService validationService)
        {
            _repository = repository;
            _validationService = validationService;
        }

        public async Task<List<ServiceTypeDto>> GetAllServiceTypesAsync()
        {
            var serviceTypes = await _repository.GetAllAsync();
            return serviceTypes.Select(s => new ServiceTypeDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                IntentionServices = s.IntentionServices != null && s.IntentionServices.Count() > 0 ? s.IntentionServices.Select(x => new IntentionServiceDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ServiceTypeId = x.ServiceTypeId,
                    Daily_Price = x.Daily_Price,
                    Limit_Area = x.Limit_Area,
                    UrbanConfrontation = x.UrbanConfrontation,
                    RuralConfrontation = x.RuralConfrontation
                }).ToList() : new List<IntentionServiceDto>()
            }).ToList();
        }

        public async Task<ServiceTypeDto> GetServiceTypeByIdAsync(int id)
        {
            var serviceType = await _repository.GetByIdAsync(id);
            if (serviceType == null)
                throw new Exception($"ServiceType with ID {id} not found");

            return new ServiceTypeDto
            {
                Id = serviceType.Id,
                Name = serviceType.Name,
                Description = serviceType.Description,
                IntentionServices = serviceType.IntentionServices?.Select(x => new IntentionServiceDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ServiceTypeId = x.ServiceTypeId,
                    Daily_Price = x.Daily_Price,
                    Limit_Area = x.Limit_Area,
                    UrbanConfrontation = x.UrbanConfrontation,
                    RuralConfrontation = x.RuralConfrontation
                }).ToList()
            };
        }

        public async Task<ServiceTypeDto> CreateServiceTypeAsync(ServiceTypeRequest serviceTypeRequest)
        {
            // Validate the service type
            _validationService.ValidateServiceType(serviceTypeRequest);

            var serviceType = new ServiceTypeDbo
            {
                Name = serviceTypeRequest.Name,
                Description = serviceTypeRequest.Description,
                IsActive = true
            };

            var createdServiceType = await _repository.CreateAsync(serviceType);
            return new ServiceTypeDto
            {
                Id = createdServiceType.Id,
                Name = createdServiceType.Name,
                Description = createdServiceType.Description,
                IntentionServices = new List<IntentionServiceDto>()
            };
        }

        public async Task UpdateServiceTypeAsync(int id, ServiceTypeRequest serviceTypeRequest)
        {
            // Validate the service type
            _validationService.ValidateServiceType(serviceTypeRequest);

            var existingServiceType = await _repository.GetByIdAsync(id);
            if (existingServiceType == null)
                throw new KeyNotFoundException($"ServiceType with ID {id} not found");

            // Update relevant properties
            existingServiceType.Name = serviceTypeRequest.Name;
            existingServiceType.Description = serviceTypeRequest.Description;

            // Update the database
            await _repository.UpdateAsync(existingServiceType);
        }

        public async Task DeleteServiceTypeAsync(int id)
        {
            var existingServiceType = await _repository.GetByIdAsync(id);
            if (existingServiceType == null)
                throw new KeyNotFoundException($"ServiceType with ID {id} not found");

            // Check if there are any associated budgets
            bool isAssociated = await _repository.IsAssociatedWithBudgetAsync(id);

            if (isAssociated)
            {
                // Just deactivate if there are associated budgets
                await _repository.DeactivateAsync(id);
            }
            else
            {
                // Otherwise, delete completely
                await _repository.DeleteAsync(id);
            }
        }

        public async Task<List<ServiceTypeRequest>> GetServiceTypesByBudgetIdAsync(int budgetId)
        {
            var serviceTypes = await _repository.GetByBudgetIdAsync(budgetId);
            if (serviceTypes == null || !serviceTypes.Any())
                throw new KeyNotFoundException($"No ServiceTypes found for Budget with ID {budgetId}");

            return serviceTypes.Select(s => new ServiceTypeRequest
            {
                Name = s.Name,
                Description = s.Description
            }).ToList();
        }
    }
}