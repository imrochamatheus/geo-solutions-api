using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.Models;
using GeoSolucoesAPI.Repositories.Interfaces;
using GeoSolucoesAPI.Services.Interfaces;

namespace GeoSolucoesAPI.Services
{
    public class StartPointService : IStartPointService
    {
        private readonly IStartPointRepository _repository;

        public StartPointService(IStartPointRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StartPointDbo>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<StartPointDbo?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task<StartPointDbo> CreateAsync(StartPointDto dto)
        {
            var entity = new StartPointDbo
            {
                Street = dto.Street,
                Number = dto.Number,
                Neighborhood = dto.Neighborhood,
                City = dto.City,
                State = dto.State,
                Country = dto.Country
            };

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(int id, StartPointDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Street = dto.Street;
            existing.Number = dto.Number;
            existing.Neighborhood = dto.Neighborhood;
            existing.City = dto.City;
            existing.State = dto.State;
            existing.Country = dto.Country;

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
