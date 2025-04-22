using GeoSolucoesAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace GeoSolucoesAPI.DTOs
{
    public class BudgetUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Cell { get; set; }
        public UserType UserType { get; set; }
    }
}
