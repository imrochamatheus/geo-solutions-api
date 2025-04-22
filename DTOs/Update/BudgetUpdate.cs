using GeoSolucoesAPI.DTOs.Request;
using GeoSolucoesAPI.Helpers;
using ServiceManagement.DTOs;

namespace GeoSolucoesAPI.DTOs.Update
{
    public class BudgetUpdate
    {
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AreaParameters BudgetAreaSettings { get; set; }
        public int Confrontations { get; set; } = 0;
        public int IntentionServiceId { get; set; }
        public int ServiceTypeId { get; set; }
        public virtual AddressRequest Address { get; set; }
    }
}
