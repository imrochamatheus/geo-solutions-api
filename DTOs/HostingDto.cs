using System.ComponentModel.DataAnnotations;

namespace GeoSolucoesAPI.DTOs
{
    public class HostingDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'price' é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O campo 'distanteMin' é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A distancia mínima deve ser maior que zero.")]
        public float DistanteMin { get; set; }

        [Required(ErrorMessage = "O campo 'distanteMax' é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A distancia máxima deve ser maior que zero.")]
        public float DistanteMax { get; set; }
    }
}
