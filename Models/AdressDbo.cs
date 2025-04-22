using GeoSolucoesAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceManagement.Models
{
    public class AddressDbo
    {
        public int Id { get; set; }

        public int? UserId { get; set; } = 0;
        public int BudgetId { get; set; } = 0;

        public string Zipcode { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Number { get; set; }
        public string Complement { get; set; }
    }
}