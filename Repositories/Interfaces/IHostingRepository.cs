using GeoSolucoesAPI.Models;

namespace GeoSolucoesAPI.Repositories.Interfaces
{
    public interface IHostingRepository
    {
        Task<IEnumerable<HostingDbo>> GetAllAsync();
        Task<HostingDbo?> GetByIdAsync(int id);
        Task AddAsync(HostingDbo entity);
        void Update(HostingDbo entity);
        void Delete(HostingDbo entity);
        Task SaveChangesAsync();
    }
}
