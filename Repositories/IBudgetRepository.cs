using GeoSolucoesAPI.Models;
using ServiceManagement.Models;

namespace GeoSolucoesAPI.Repositories
{
    public interface IBudgetRepository
    {
        Task<BudgetDbo> PostBudget(BudgetDbo budgetCandidate);
        Task<BudgetDbo> GetBudgetById(int budgetId);
        Task<BudgetDbo> UpdateBudget(BudgetDbo budgetToUpdate);
        Task<List<BudgetDbo>> GetAllBudgets();
        Task DeleteBudget(BudgetDbo budgetId);
        Task<StartPointDbo> GetStartPoint();
        Task<List<ConfrontationDbo>> GetConfrontations();
        Task<List<HostingDbo>> GetHosting();
        Task<List<DistanceDbo>> GetDistance();
    }
}
