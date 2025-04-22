using GeoSolucoesAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using ServiceManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GeoSolucoesAPI.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [JsonIgnore]
        public string Password { get; set; }

        [Required]
        public string Cell {  get; set; }

        [Required] 
        public UserType UserType { get; set; } = UserType.User; 

        public List<BudgetDbo> Budgets { get; set; }

    }

    public enum UserType
    {
        [Display(Name = "Admin")]
        Admin = 1,
        [Display(Name = "User")]
        User = 2
    }
}
