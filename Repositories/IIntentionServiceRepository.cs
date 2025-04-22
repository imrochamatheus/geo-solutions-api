using ServiceManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceManagement.Repositories
{
    public interface IIntentionServiceRepository
    {
        Task<List<IntentionServiceDbo>> GetAllAsync();
        Task<IntentionServiceDbo> GetByIdAsync(int id);
        Task<IntentionServiceDbo> CreateAsync(IntentionServiceDbo intentionService);
        Task UpdateAsync(IntentionServiceDbo intentionService);
        Task DeleteAsync(int id);
        Task<List<IntentionServiceDbo>> GetByServiceTypeIdAsync(int serviceTypeId);
        Task<bool> IsAssociatedWithBudgetAsync(int id);
        Task DeactivateAsync(int id);
    }
}