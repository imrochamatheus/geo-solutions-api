using GeoSolucoesAPI.Models;
using GeoSolucoesAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoSolucoesAPI.Repositories
{
    public class DistanceRepository : IDistanceRepository
    {
        private readonly GeoSolutionsDbContext _context;

        public DistanceRepository(GeoSolutionsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DistanceDbo>> GetAllAsync()
            => await _context.Distances.ToListAsync();

        public async Task<DistanceDbo?> GetByIdAsync(int id)
            => await _context.Distances.FindAsync(id);

        public async Task AddAsync(DistanceDbo entity)
            => await _context.Distances.AddAsync(entity);

        public void Update(DistanceDbo entity)
            => _context.Distances.Update(entity);

        public void Delete(DistanceDbo entity)
            => _context.Distances.Remove(entity);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
