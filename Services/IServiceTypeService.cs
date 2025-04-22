using GeoSolucoesAPI.DTOs;
using ServiceManagement.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceManagement.Services
{
    public interface IServiceTypeService
    {
        Task<List<ServiceTypeDto>> GetAllServiceTypesAsync();
        Task<ServiceTypeDto> GetServiceTypeByIdAsync(int id);
        Task<ServiceTypeDto> CreateServiceTypeAsync(ServiceTypeRequest serviceTypeRequest);
        Task UpdateServiceTypeAsync(int id, ServiceTypeRequest serviceTypeRequest);
        Task DeleteServiceTypeAsync(int id);
        Task<List<ServiceTypeRequest>> GetServiceTypesByBudgetIdAsync(int budgetId);
    }
}