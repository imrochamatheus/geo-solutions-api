using System.ComponentModel.DataAnnotations;

namespace GeoSolucoesAPI.DTOs
{
    public class StartPointDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'street' é obrigatório.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo 'number' é obrigatório.")]
        public string Number { get; set; } 

        [Required(ErrorMessage = "O campo 'city' é obrigatório.")]
        public string City { get; set; } 

        [Required(ErrorMessage = "O campo 'state' é obrigatório.")]
        public string State { get; set; } 
        
        [Required(ErrorMessage = "O campo 'country' é obrigatório.")]
        public string Country { get; set; } 

        [Required(ErrorMessage = "O campo 'neighborhood' é obrigatório.")]
        public string Neighborhood { get; set; } 
    }
}
