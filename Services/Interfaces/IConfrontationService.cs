using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.Models;

namespace GeoSolucoesAPI.Services.Interfaces
{
    public interface IConfrontationService
    {
        Task<IEnumerable<ConfrontationDbo>> GetAllAsync();
        Task<ConfrontationDbo?> GetByIdAsync(int id);
        Task<ConfrontationDbo> CreateAsync(ConfrontationDto dto);
        Task<bool> UpdateAsync(int id, ConfrontationDto dto);
        Task<bool> DeleteAsync(int id);
    }
}