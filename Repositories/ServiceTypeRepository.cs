using GeoSolucoesAPI.Models;
using Microsoft.EntityFrameworkCore;
using ServiceManagement.Models;

namespace ServiceManagement.Repositories
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly GeoSolutionsDbContext _context;

        public ServiceTypeRepository(GeoSolutionsDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceTypeDbo>> GetAllAsync()
        {
            return await _context.ServiceTypes
                .Where(s => s.IsActive) // Only get active service types
                .Include(s => s.IntentionServices.Where(i => i.IsActive)) // Only include active intention services
                .ToListAsync();
        }

        public async Task<ServiceTypeDbo> GetByIdAsync(int id)
        {
            return await _context.ServiceTypes
                .Where(s => s.Id == id && s.IsActive)
                .Include(s => s.IntentionServices.Where(i => i.IsActive))
                .FirstOrDefaultAsync();
        }

        public async Task<ServiceTypeDbo> CreateAsync(ServiceTypeDbo serviceType)
        {
            serviceType.IsActive = true;
            _context.ServiceTypes.Add(serviceType);
            await _context.SaveChangesAsync();
            return serviceType;
        }

        public async Task UpdateAsync(ServiceTypeDbo serviceType)
        {
            _context.Entry(serviceType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var serviceType = await _context.ServiceTypes.FindAsync(id);
            if (serviceType != null)
            {
                _context.ServiceTypes.Remove(serviceType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ServiceTypeDbo>> GetByBudgetIdAsync(int budgetId)
        {
            return await _context.ServiceTypes
                 .Where(s => s.IsActive && (s.BudgetId == budgetId || budgetId == 0))
                .Include(s => s.IntentionServices.Where(i => i.IsActive))
                .ToListAsync();
        }

        public async Task<bool> IsAssociatedWithBudgetAsync(int id)
        {
            // Check if this service type is associated with any budget
            return await _context.ServiceTypes
                .AnyAsync(s => s.Id == id && s.BudgetId.HasValue);
        }

        public async Task DeactivateAsync(int id)
        {
            var serviceType = await _context.ServiceTypes.FindAsync(id);
            if (serviceType != null)
            {
                serviceType.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}