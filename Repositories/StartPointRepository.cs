using GeoSolucoesAPI.Models;
using GeoSolucoesAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoSolucoesAPI.Repositories
{
    public class StartPointRepository : IStartPointRepository
    {
        private readonly GeoSolutionsDbContext _context;

        public StartPointRepository(GeoSolutionsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StartPointDbo>> GetAllAsync()
            => await _context.StartPoints.ToListAsync();

        public async Task<StartPointDbo?> GetByIdAsync(int id)
            => await _context.StartPoints.FindAsync(id);

        public async Task AddAsync(StartPointDbo entity)
            => await _context.StartPoints.AddAsync(entity);

        public void Update(StartPointDbo entity)
            => _context.StartPoints.Update(entity);

        public void Delete(StartPointDbo entity)
            => _context.StartPoints.Remove(entity);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
