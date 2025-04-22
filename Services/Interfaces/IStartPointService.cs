using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.Models;


namespace GeoSolucoesAPI.Services.Interfaces
{
    public interface IStartPointService
    {
        Task<IEnumerable<StartPointDbo>> GetAllAsync();
        Task<StartPointDbo?> GetByIdAsync(int id);
        Task<StartPointDbo> CreateAsync(StartPointDto dto);
        Task<bool> UpdateAsync(int id, StartPointDto dto);
        Task<bool> DeleteAsync(int id);
    }
}