using ServiceManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceManagement.Repositories
{
    public interface IServiceTypeRepository
    {
        Task<List<ServiceTypeDbo>> GetAllAsync();
        Task<ServiceTypeDbo> GetByIdAsync(int id);
        Task<ServiceTypeDbo> CreateAsync(ServiceTypeDbo serviceType);
        Task UpdateAsync(ServiceTypeDbo serviceType);
        Task DeleteAsync(int id);
        Task<List<ServiceTypeDbo>> GetByBudgetIdAsync(int budgetId);
        Task<bool> IsAssociatedWithBudgetAsync(int id);
        Task DeactivateAsync(int id);
    }
}