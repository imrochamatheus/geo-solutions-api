using GeoSolucoesAPI.Custom;
using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.DTOs.BudgetResponse;
using GeoSolucoesAPI.DTOs.Request;
using GeoSolucoesAPI.DTOs.Update;
using GeoSolucoesAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoSolucoesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetsController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpPost("calc")]
        [ProducesResponseType(typeof(CalcResponse), 200)]
        public async Task<IActionResult> ProcessCalculation(CalcParameters calcParameters)
        {
            var finalPrice = await _budgetService.ProcessCalc(calcParameters);
            return Ok(finalPrice); ;
        }


        [HttpPost]
        [ProducesResponseType(typeof(BudgetDto), 200)]
        public async Task<IActionResult> PostBudget(BudgetRequest newBudget)
        {
            var budgetCreated = await _budgetService.PostBudget(newBudget);
            return Ok(budgetCreated);
        }

        [HttpGet]
        [ProducesResponseType(typeof(BudgetDto), 200)]
        public async Task<IActionResult> GetBudgetById(int budgetId)
        {
            var budget = await _budgetService.GetBudgetById(budgetId);
            return Ok(budget);
        }

        [HttpPut("{budgetId}")]
        [ProducesResponseType(typeof(BudgetDto), 200)]
        public async Task<IActionResult> UpdateBudget(int budgetId, [FromBody]BudgetUpdate budgetToUpdate)
        {
            var budgetUpdated = await _budgetService.UpdateBudget(budgetId, budgetToUpdate);
            return Ok(budgetUpdated);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(RemovedItem), 200)]
        public async Task<IActionResult> DeleteBudget(int budgetId)
        {
            var budgetRemoved = await _budgetService.DeleteBudget(budgetId);
            return Ok(budgetRemoved);
        }

        [HttpGet("All")]
        [ProducesResponseType(typeof(List<BudgetDto>), 200)]
        public async Task<IActionResult> GetAllBudgets()
        {
            var budgetList = await _budgetService.GetAllBudgets();
            return Ok(budgetList);
        }

        [HttpGet("address")]
        [ProducesResponseType(typeof(AddressResponse), 200)]
        public async Task<IActionResult> GetAddressByCep(string cep)
        {
            var address = await _budgetService.GetAddressByCep(cep);
            return Ok(address);
        }

    }

}
