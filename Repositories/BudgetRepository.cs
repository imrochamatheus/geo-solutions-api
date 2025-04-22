using GeoSolucoesAPI.Models;
using ServiceManagement.Models;

namespace GeoSolucoesAPI.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly GeoSolutionsDbContext _context;

        public BudgetRepository(GeoSolutionsDbContext context)
        {
            _context = context;
        }

        public async Task<StartPointDbo> GetStartPoint()
        {
            var startPoints = _context.StartPoints.FirstOrDefault();
            return startPoints;
        }

        public async Task<List<ConfrontationDbo>> GetConfrontations()
        {
            var startPoints = _context.Confrontations.ToList();
            return startPoints;
        }

        public async Task<HostingDbo> GetHosting()
        {
            var hosting = _context.Hostings.FirstOrDefault();
            return hosting;
        }

        public async Task<List<DistanceDbo>> GetDistance()
        {
            var distance = _context.Distances.ToList();
            return distance;
        }

        public async Task<BudgetDbo> PostBudget(BudgetDbo budgetCandidate)
        {

            await _context.Budgets.AddAsync(budgetCandidate);
            await _context.SaveChangesAsync();

            var budgetCreated = _context.GetBudgetFull().FirstOrDefault(x => x.Id == budgetCandidate.Id);
            return budgetCreated;
        }

        public async Task DeleteBudget(BudgetDbo budgetToDelete)
        {
            if(budgetToDelete is not null)
                 _context.Budgets.Remove(budgetToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<BudgetDbo> GetBudgetById(int budgetId)
        {
            var budget = _context.GetBudgetFull().FirstOrDefault(x => x.Id == budgetId);
            return budget;
        }

        public async Task<List<BudgetDbo>> GetAllBudgets()
        {
            var budgetsList = _context.GetBudgetFull().ToList();
            return budgetsList;
        }

        public async Task<BudgetDbo> UpdateBudget(BudgetDbo budgetToUpdate)
        {
            var budgetUpdated = _context.Budgets.Update(budgetToUpdate);
            await _context.SaveChangesAsync();
            return budgetToUpdate;
        }
    }
}
