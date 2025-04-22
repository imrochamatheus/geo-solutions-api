using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.Models;
using GeoSolucoesAPI.Repositories.Interfaces;
using GeoSolucoesAPI.Services.Interfaces;

namespace GeoSolucoesAPI.Services
{
    public class DistanceService : IDistanceService
    {
        private readonly IDistanceRepository _repository;

        public DistanceService(IDistanceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DistanceDbo>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<DistanceDbo?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task<DistanceDbo> CreateAsync(DistanceDto dto)
        {
            var entity = new DistanceDbo
            {
                AreaMin = dto.AreaMin,
                AreaMax = dto.AreaMax,
                Multiplier = dto.Multiplier
            };

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> UpdateAsync(int id, DistanceDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.AreaMin = dto.AreaMin;
            existing.AreaMax = dto.AreaMax;
            existing.Multiplier = dto.Multiplier;

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
