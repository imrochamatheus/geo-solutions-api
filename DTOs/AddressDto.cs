using GeoSolucoesAPI.Models;
using ServiceManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoSolucoesAPI.DTOs
{
    public class AddressDto
    {
        public int Id { get; set; }

        public int? UserId { get; set; } = 0;
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int BudgetId { get; set; } = 0;

        [ForeignKey("BudgetId")]
        public BudgetDbo Budget { get; set; }

        public string Zipcode { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Number { get; set; }
        public string Complement { get; set; }
    }
}
