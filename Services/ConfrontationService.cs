using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.Models;
using GeoSolucoesAPI.Repositories.Interfaces;
using GeoSolucoesAPI.Services.Interfaces;

namespace GeoSolucoesAPI.Services
{
    public class ConfrontationService : IConfrontationService
    {
        private readonly IConfrontationRepository _repository;

        public ConfrontationService(IConfrontationRepository repository)
        { _repository = repository; }

        public async Task<IEnumerable<ConfrontationDbo>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<ConfrontationDbo?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task<ConfrontationDbo> CreateAsync(ConfrontationDto dto)
        {
            var entity = new ConfrontationDbo
            {
                UrbanConfrontation = dto.UrbanConfrontation,
                RuralConfrontation = dto.RuralConfrontation,
                AreaMin = dto.AreaMin,
                AreaMax = dto.AreaMax,
                Price = dto.Price
            };

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> UpdateAsync(int id, ConfrontationDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.UrbanConfrontation = dto.UrbanConfrontation;
            existing.RuralConfrontation = dto.RuralConfrontation;
            existing.AreaMin = dto.AreaMin;
            existing.AreaMax = dto.AreaMax;
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
