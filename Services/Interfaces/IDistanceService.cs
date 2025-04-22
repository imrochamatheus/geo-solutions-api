using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.Models;

namespace GeoSolucoesAPI.Services.Interfaces
{
    public interface IDistanceService
    {
        Task<IEnumerable<DistanceDbo>> GetAllAsync();
        Task<DistanceDbo?> GetByIdAsync(int id);
        Task<DistanceDbo> CreateAsync(DistanceDto dto);
        Task<bool> UpdateAsync(int id, DistanceDto dto);
        Task<bool> DeleteAsync(int id);
    }
}