using GeoSolucoesAPI.Models;
using GeoSolucoesAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoSolucoesAPI.Repositories
{
    public class HostingRepository : IHostingRepository
    {
        private readonly GeoSolutionsDbContext _context;

        public HostingRepository(GeoSolutionsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HostingDbo>> GetAllAsync()
            => await _context.Hostings.ToListAsync();

        public async Task<HostingDbo?> GetByIdAsync(int id)
            => await _context.Hostings.FindAsync(id);

        public async Task AddAsync(HostingDbo entity)
            => await _context.Hostings.AddAsync(entity);

        public void Update(HostingDbo entity)
            => _context.Hostings.Update(entity);

        public void Delete(HostingDbo entity)
            => _context.Hostings.Remove(entity);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
