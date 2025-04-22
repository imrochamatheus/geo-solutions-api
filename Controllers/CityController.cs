using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoSolucoesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CityDto>>> GetAll()
        {
            var cities = await _cityService.GetAllCitiesAsync();
            return Ok(cities);
        }

        //[HttpGet("search")]
        //public async Task<ActionResult<List<CityDto>>> Search([FromQuery] string query)
        //{
        //    if (string.IsNullOrWhiteSpace(query))
        //    {
        //        return BadRequest("A consulta é obrigatória.");
        //    }

        //    var cities = await _cityService.SearchCitiesAsync(query);
        //    return Ok(cities);
        //}

        [HttpPost]
        public async Task<ActionResult> Add(CityDto cityDto)
        {
            var validationErrors = ValidationService.CityValidation(cityDto);
            if (validationErrors.Any())
            {
                return BadRequest(string.Join(" | ", validationErrors));
            }

            var result = await _cityService.AddCityAsync(cityDto);
            if (result == null)
            {
                return Conflict("Essa cidade já está cadastrada.");
            }

            return CreatedAtAction(nameof(GetAll), result); 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var result = await _cityService.RemoveCityAsync(id);
            if (result == null)
            {
                return NotFound("Cidade não encontrada.");
            }

            return NoContent();
        }

        [HttpGet("coverage")]
        public async Task<ActionResult<bool>> CheckCoverage([FromQuery] string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                return BadRequest("O nome da cidade é obrigatório.");
            }

            var isCovered = await _cityService.IsCityCoveredAsync(cityName);
            return Ok(isCovered);
        }
    }
}