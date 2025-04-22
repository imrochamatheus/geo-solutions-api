using System.ComponentModel.DataAnnotations;

namespace GeoSolucoesAPI.DTOs
{
    public class DistanceDto
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "O campo 'areaMin' é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A área mínima deve ser maior que zero.")]
        public decimal AreaMin { get; set; }

        [Required(ErrorMessage = "O campo 'areaMax' é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A área máxima deve ser maior que zero.")]
        public decimal AreaMax { get; set; }

        [Required(ErrorMessage = "O campo 'multiplier' é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O multiplicador deve ser maior que zero")]
        public decimal Multiplier { get; set; }
   
    }  
}
