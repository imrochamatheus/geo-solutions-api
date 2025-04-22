using GeoSolucoesAPI.Helpers;
using GeoSolucoesAPI.Models;
using ServiceManagement.DTOs;
using ServiceManagement.Models;

namespace GeoSolucoesAPI.DTOs.BudgetResponse
{
    public class BudgetDto
    {
        public int Id { get; set; }
        public virtual BudgetUser User { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AreaParameters BudgetAreaSettings { get; set; }
        public int Confrontations { get; set; }
        public virtual IntentionServiceResponse IntentionService { get; set; }
        public virtual ServiceTypeResponse ServiceType { get; set; }
        public virtual AddressResponse Address { get; set; }
    }
}
