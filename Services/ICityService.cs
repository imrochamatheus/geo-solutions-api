using GeoSolucoesAPI.Custom;
using GeoSolucoesAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoSolucoesAPI.Services
{
    public interface ICityService
    {
        Task<List<CityDto>> GetAllCitiesAsync();
        //Task<List<CityDto>> SearchCitiesAsync(string query);
        Task<CityDto> AddCityAsync(CityDto cityDto);
        Task<RemovedItem> RemoveCityAsync(int id);
        Task<bool> IsCityCoveredAsync(int id);
    }
}