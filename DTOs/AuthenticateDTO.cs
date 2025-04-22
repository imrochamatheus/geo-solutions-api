using System.ComponentModel.DataAnnotations;

namespace GeoSolucoesAPI.DTOs
{
    public class authenticateDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
