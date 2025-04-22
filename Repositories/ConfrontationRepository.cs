using GeoSolucoesAPI.Models;
using Microsoft.EntityFrameworkCore;
using GeoSolucoesAPI.Repositories.Interfaces;

namespace GeoSolucoesAPI.Repositories
{
    public class ConfrontationRepository : IConfrontationRepository
    {
        private readonly GeoSolutionsDbContext _context;

        public ConfrontationRepository(GeoSolutionsDbContext context)
        { _context = context;}

        public async Task<IEnumerable<ConfrontationDbo>> GetAllAsync()
            => await _context.Confrontations.ToListAsync();

        public async Task<ConfrontationDbo?> GetByIdAsync(int id)
            => await _context.Confrontations.FindAsync(id);

        public async Task AddAsync(ConfrontationDbo entity)
            => await _context.Confrontations.AddAsync(entity);

        public void Update(ConfrontationDbo entity)
            => _context.Confrontations.Update(entity);

        public void Delete(ConfrontationDbo entity)
            => _context.Confrontations.Remove(entity);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
