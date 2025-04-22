using GeoSolucoesAPI.Models;

namespace GeoSolucoesAPI.Repositories.Interfaces
{
    public interface IDistanceRepository
    {
        Task<IEnumerable<DistanceDbo>> GetAllAsync();
        Task<DistanceDbo?> GetByIdAsync(int id);
        Task AddAsync(DistanceDbo entity);
        void Update(DistanceDbo entity);
        void Delete(DistanceDbo entity);
        Task SaveChangesAsync();
    }
}
