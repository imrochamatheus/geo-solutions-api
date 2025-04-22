using GeoSolucoesAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GeoSolucoesAPI.DTOs
{
    public class UserDTO
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(8, ErrorMessage =  "A senha deve ter no mínimo 8 caracteres.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O número de celular é obrigatório.")]
        [RegularExpression(@"^\(?\d{2}\)?\s?9\d{4}-?\d{4}$", ErrorMessage = "Número de celular inválido. Ex: (61) 96772-8467")]
        public string Cell { get; set; }

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        [Range(1, 2, ErrorMessage = "Tipo de usuário inválido.")]
        public UserType UserType { get; set; }

    }

}
