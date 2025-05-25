using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoSolucoesAPI.Models
{
    [Table("Forgot")]
    public class Forgot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "O código é obrigatório.")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "O código deve conter exatamente 5 dígitos.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "A data/hora da solicitação é obrigatória.")]
        public DateTime RequestedAt { get; set; }
    }
}
