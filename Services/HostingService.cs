using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.Models;
using GeoSolucoesAPI.Repositories.Interfaces;
using GeoSolucoesAPI.Services.Interfaces;

namespace GeoSolucoesAPI.Services
{
    public class HostingService : IHostingService
    {
        private readonly IHostingRepository _repository;

        public HostingService(IHostingRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<HostingDbo>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<HostingDbo?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task<HostingDbo> CreateAsync(HostingDto dto)
        {
            var entity = new HostingDbo
            {
                DistanteMin = dto.DistanteMin,
                DistanteMax = dto.DistanteMax,
                Price = dto.Price
            };

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> UpdateAsync(int id, HostingDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.DistanteMin = dto.DistanteMin;
            existing.DistanteMax = dto.DistanteMax;
            existing.Price = dto.Price;

            _repository.Update(existing);
            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            _repository.Delete(existing);
            await _repository.SaveChangesAsync();
            
            return true;
        }
    }
}
