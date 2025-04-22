using GeoSolucoesAPI.Models;
using Microsoft.EntityFrameworkCore;
using ServiceManagement.Models;

namespace ServiceManagement.Repositories
{
    public class IntentionServiceRepository : IIntentionServiceRepository
    {
        private readonly GeoSolutionsDbContext _context;

        public IntentionServiceRepository(GeoSolutionsDbContext context)
        {
            _context = context;
        }

        public async Task<List<IntentionServiceDbo>> GetAllAsync()
        {
            return await _context.IntentionServices
                .Where(i => i.IsActive) // Only get active intention services
                .Include(i => i.ServiceType)
                .ToListAsync();
        }

        public async Task<IntentionServiceDbo> GetByIdAsync(int id)
        {
            return await _context.IntentionServices
                .Include(i => i.ServiceType)
                .FirstOrDefaultAsync(i => i.Id == id && i.IsActive);
        }

        public async Task<IntentionServiceDbo> CreateAsync(IntentionServiceDbo intentionService)
        {
            intentionService.IsActive = true;
            _context.IntentionServices.Add(intentionService);
            await _context.SaveChangesAsync();
            return intentionService;
        }

        public async Task UpdateAsync(IntentionServiceDbo intentionService)
        {
            _context.Entry(intentionService).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var intentionService = await _context.IntentionServices.FindAsync(id);
            if (intentionService != null)
            {
                _context.IntentionServices.Remove(intentionService);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<IntentionServiceDbo>> GetByServiceTypeIdAsync(int serviceTypeId)
        {
            return await _context.IntentionServices
                .Where(i => i.ServiceTypeId == serviceTypeId && i.IsActive)
                .ToListAsync();
        }

        public async Task<bool> IsAssociatedWithBudgetAsync(int id)
        {
            // Check if there are any budgets using this intention service
            return await _context.Budgets
                .AnyAsync(b => b.IntentionServiceId == id);
        }

        public async Task DeactivateAsync(int id)
        {
            var intentionService = await _context.IntentionServices.FindAsync(id);
            if (intentionService != null)
            {
                intentionService.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}