using GeoSolucoesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoSolucoesAPI.Repositories
{
    public interface ICityRepository
    {
        Task<List<CityDbo>> GetAllAsync();
        //Task<List<City>> SearchAsync(string query);
        Task<CityDbo> GetByNameAsync(string name);
        Task<CityDbo> GetByIdAsync(int id);
        Task AddAsync(CityDbo city);
        Task DeleteAsync(int id);
    }
}