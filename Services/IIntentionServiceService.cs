using ServiceManagement.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceManagement.Services
{
    public interface IIntentionServiceService
    {
        Task<List<IntentionServiceDto>> GetAllIntentionServicesAsync();
        Task<IntentionServiceDto> GetIntentionServiceByIdAsync(int id);
        Task<IntentionServiceDto> CreateIntentionServiceAsync(IntentionServiceDto intentionServiceDto);
        Task UpdateIntentionServiceAsync(int id, IntentionServiceDto intentionServiceDto);
        Task DeleteIntentionServiceAsync(int id);
        Task<List<IntentionServiceDto>> GetIntentionServicesByServiceTypeIdAsync(int serviceTypeId);
    }
}