using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.DTOs.BudgetResponse;
using GeoSolucoesAPI.Services.Validations;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.DTOs;
using ServiceManagement.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private readonly IServiceTypeService _service;

        public ServiceTypeController(IServiceTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ServiceTypeDto), 200)]
        public async Task<ActionResult<IEnumerable<ServiceTypeDto>>> GetAll()
        {
            var serviceTypes = await _service.GetAllServiceTypesAsync();
            return Ok(serviceTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceTypeDto>> GetById(int id)
        {
            try
            {
                var serviceType = await _service.GetServiceTypeByIdAsync(id);
                return Ok(serviceType);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ServiceTypeDto>> Create(ServiceTypeRequest request)
        {
            try
            {
                var createdServiceType = await _service.CreateServiceTypeAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = createdServiceType.Id }, createdServiceType);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ServiceTypeRequest serviceTypeRequest)
        {
            try
            {
                await _service.UpdateServiceTypeAsync(id, serviceTypeRequest);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteServiceTypeAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("budget/{budgetId}")]
        public async Task<ActionResult<IEnumerable<ServiceTypeRequest>>> GetByBudgetId(int budgetId)
        {
            try
            {
                var serviceTypes = await _service.GetServiceTypesByBudgetIdAsync(budgetId);
                return Ok(serviceTypes);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}