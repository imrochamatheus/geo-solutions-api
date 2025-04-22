using GeoSolucoesAPI.Models;

namespace GeoSolucoesAPI.Repositories.Interfaces
{
    public interface IStartPointRepository
    {
        Task<IEnumerable<StartPointDbo>> GetAllAsync();
        Task<StartPointDbo?> GetByIdAsync(int id);
        Task AddAsync(StartPointDbo entity);
        void Update(StartPointDbo entity);
        void Delete(StartPointDbo entity);
        Task SaveChangesAsync();
    }
}
