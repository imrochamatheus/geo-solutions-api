using GeoSolucoesAPI.Services.Validations;
using Microsoft.AspNetCore.Authorization;
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
    public class IntentionServiceController : ControllerBase
    {
        private readonly IIntentionServiceService _service;

        public IntentionServiceController(IIntentionServiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IntentionServiceDto>>> GetAll()
        {
            var intentionServices = await _service.GetAllIntentionServicesAsync();
            return Ok(intentionServices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IntentionServiceDto>> GetById(int id)
        {
            var intentionService = await _service.GetIntentionServiceByIdAsync(id);
            if (intentionService == null)
                return NotFound();

            return Ok(intentionService);
        }

        [HttpPost]
        public async Task<ActionResult<IntentionServiceDto>> Create(IntentionServiceDto intentionServiceDto)
        {
            try
            {
                var createdIntentionService = await _service.CreateIntentionServiceAsync(intentionServiceDto);
                return CreatedAtAction(nameof(GetById), new { id = createdIntentionService.Id }, createdIntentionService);
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
        public async Task<IActionResult> Update(int id, IntentionServiceDto intentionServiceDto)
        {
            try
            {
                await _service.UpdateIntentionServiceAsync(id, intentionServiceDto);
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
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteIntentionServiceAsync(id);
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

        [HttpGet("servicetype/{serviceTypeId}")]
        public async Task<ActionResult<IEnumerable<IntentionServiceDto>>> GetByServiceTypeId(int serviceTypeId)
        {
            var intentionServices = await _service.GetIntentionServicesByServiceTypeIdAsync(serviceTypeId);
            return Ok(intentionServices);
        }
    }
}