using GeoSolucoesAPI.Helpers;
using GeoSolucoesAPI.Models;
using ServiceManagement.Models;

namespace GeoSolucoesAPI.DTOs.Request
{
    public class BudgetRequest
    {
        public virtual int UserId { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AreaParameters BudgetAreaSettings { get; set; }
        public int Confrontations { get; set; } = 0;
        public int ServiceTypeId { get; set; }
        public int IntentionServiceId { get; set; }
        public  AddressRequest Address { get; set; }

    }
}
