using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.Models;

namespace GeoSolucoesAPI.Services.Interfaces
{
    public interface IHostingService
    {
        Task<IEnumerable<HostingDbo>> GetAllAsync();
        Task<HostingDbo?> GetByIdAsync(int id);
        Task<HostingDbo> CreateAsync(HostingDto dto);
        Task<bool> UpdateAsync(int id, HostingDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
