using GeoSolucoesAPI.Models;
using System.Collections.Generic;

namespace GeoSolucoesAPI.Repositories.Interfaces
{
    public interface IConfrontationRepository
    {
        Task<IEnumerable<ConfrontationDbo>> GetAllAsync();
        Task<ConfrontationDbo?> GetByIdAsync(int id);
        Task AddAsync(ConfrontationDbo entity);
        void Update(ConfrontationDbo entity);
        void Delete(ConfrontationDbo entity);
        Task SaveChangesAsync();
    }
}
