using GeoSolucoesAPI.Custom;
using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.DTOs.BudgetResponse;
using GeoSolucoesAPI.DTOs.Request;
using GeoSolucoesAPI.DTOs.Update;

namespace GeoSolucoesAPI.Services
{
    public interface IBudgetService
    {
        Task<decimal> ProcessCalc(CalcParameters calcParameters);

        Task<BudgetDto> PostBudget(BudgetRequest budget);
        Task<BudgetDto> GetBudgetById(int budgetId);
        Task<BudgetDto> UpdateBudget(int budgetId, BudgetUpdate budget);

        Task<List<BudgetDto>> GetAllBudgets();
        Task<RemovedItem> DeleteBudget(int budgetId);
    }
}
