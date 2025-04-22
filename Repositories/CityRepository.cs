using GeoSolucoesAPI.Models;
using Google;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoSolucoesAPI.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly GeoSolutionsDbContext _context;

        public CityRepository(GeoSolutionsDbContext context)
        {
            _context = context;
        }

        public async Task<List<CityDbo>> GetAllAsync()
        {
            return await _context.Cities
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        //public async Task<List<City>> SearchAsync(string query)
        //{
        //    return await _context.Cities
        //        .Where(c => c.Name.Contains(query))
        //        .OrderBy(c => c.Name)
        //        .Take(10)
        //        .ToListAsync();
        //}

        public async Task<CityDbo> GetByNameAsync(string name)
        {
            return await _context.Cities
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
        }

        public async Task<CityDbo> GetByIdAsync(int id) 
        {
            return await _context.Cities
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(CityDbo city)
        {
            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
            }
        }
    }
}