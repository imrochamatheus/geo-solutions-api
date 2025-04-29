using GeoSolucoesAPI.Custom;
using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.Models;
using GeoSolucoesAPI.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GeoSolucoesAPI.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public CityService(ICityRepository cityRepository, IHttpClientFactory httpClientFactory)
        {
            _cityRepository = cityRepository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<CityDto>> GetAllCitiesAsync()
        {
            var cities = await _cityRepository.GetAllAsync();
            return cities.Select(c => new CityDto
            {
                Id = c.Id,
                Name = c.Name,
                State = c.State
            }).ToList();
        }
        //public async Task<List<CityDto>> SearchCitiesAsync(string query)
        //{
        //    if (string.IsNullOrWhiteSpace(query))
        //    {
        //        return new List<CityDto>();
        //    }

        //    try
        //    {
        //        var httpClient = _httpClientFactory.CreateClient();
        //        var response = await httpClient.GetAsync("https://servicodados.ibge.gov.br/api/v1/localidades/estados/MG/municipios");

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return await SearchLocalDatabase(query);
        //        }

        //        var content = await response.Content.ReadAsStringAsync();
        //        var ibgeCities = JsonSerializer.Deserialize<List<IBGECity>>(content);

        //        var normalizedQuery = query.Trim().ToLower();

        //        var cities = ibgeCities?
        //            .Where(c => !string.IsNullOrWhiteSpace(c.Nome) && c.Nome.Trim().ToLower().Contains(normalizedQuery))
        //            .Select(c => new CityDto
        //            {
        //                Id = c.Id,
        //                Name = c.Nome,
        //                State = "MG"
        //            })
        //            .Take(10)
        //            .ToList() ?? new List<CityDto>();

        //        return cities;
        //    }
        //    catch (Exception)
        //    {
        //        return await SearchLocalDatabase(query);
        //    }
        //}

        //private async Task<List<CityDto>> SearchLocalDatabase(string query)
        //{
        //    try
        //    {
        //        var cities = await _cityRepository.SearchAsync(query);
        //        return cities.Select(c => new CityDto
        //        {
        //            Id = c.Id,
        //            Name = c.Name,
        //            State = c.State
        //        }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw; 
        //    }
        //}

        public async Task<CityDto> AddCityAsync(CityDto cityDto)
        {
            var existingCity = await _cityRepository.GetByNameAsync(cityDto.Name);
            if (existingCity != null)
            {
                return null;
            }

            var city = new CityDbo
            {
                Id = cityDto.Id,
                Name = cityDto.Name,
                State = cityDto.State
            };

            await _cityRepository.AddAsync(city);

            return new CityDto
            {
                Id = city.Id,
                Name = city.Name,
                State = city.State
            };
        }

        public async Task<RemovedItem> RemoveCityAsync(int id)
        {
            try
            {

                var city = await _cityRepository.GetByIdAsync(id);
                if (city == null)
                {
                    return new RemovedItem { Message = "Cidade não encontrada." };
                }

                await _cityRepository.DeleteAsync(id);

                return new RemovedItem { Message = "Cidade removida com sucesso.", ObjectName = city.Name };
            }
            catch (Exception e)
            {

                return new RemovedItem { Message = $"Falta ao deletar cidade. {e.Message}" };
            }
        }

        public async Task<bool> IsCityCoveredAsync(int ibgeId)
        {
            var city = await _cityRepository.GetByIdAsync(ibgeId);
            return city != null;
        }
    }
}