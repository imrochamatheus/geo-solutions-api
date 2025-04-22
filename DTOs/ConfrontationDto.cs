using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GeoSolucoesAPI.DTOs
{
    public class ConfrontationDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'price' é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O campo 'areaMin' é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Área mínima deve ser maior que zero.")]
        public decimal AreaMin { get; set; }

        [Required(ErrorMessage = "O campo 'areaMax' é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Área máxima deve ser maior que zero.")]
        public decimal AreaMax { get; set; }

        public bool UrbanConfrontation { get; set; }

        public bool RuralConfrontation { get; set; }
    }
}
