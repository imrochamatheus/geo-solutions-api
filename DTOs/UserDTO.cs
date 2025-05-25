using GeoSolucoesAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GeoSolucoesAPI.DTOs
{
    public class UserDTO
    {
        // public int? Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; } 

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(8, ErrorMessage =  "A senha deve ter no mínimo 8 caracteres.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O número de celular é obrigatório.")]
        [RegularExpression(@"^\(?\d{2}\)?\s?9\d{4}-?\d{4}$", ErrorMessage = "Número de celular inválido. Ex: (31) 98888-8888")]
        public string Cell { get; set; }

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        [Range(1, 2, ErrorMessage = "Tipo de usuário inválido.")]
        public UserType UserType { get; set; }

    }
    public class UpdateUserDTO
    {
        public int? Id { get; set; }

        [MinLength(2, ErrorMessage = "O nome deve ter no mínimo 2 caracteres.")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string? Email { get; set; }

        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
        public string? Password { get; set; }

        [RegularExpression(@"^\(?\d{2}\)?\s?9\d{4}-?\d{4}$", ErrorMessage = "Número de celular inválido. Ex: (31) 98888-8888")]
        public string? Cell { get; set; }

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        [Range(1, 2, ErrorMessage = "Tipo de usuário inválido.")]
        public UserType UserType { get; set; }
    }

    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "A senha atual é obrigatória.")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "A nova senha é obrigatória.")]
        [MinLength(8, ErrorMessage = "A nova senha deve ter no mínimo 8 caracteres.")]
        public string NewPassword { get; set; }
    }
    public class ForgotPasswordDTO
    {
        [MinLength(8, ErrorMessage = "A nova senha deve ter no mínimo 8 caracteres.")]
        public string NewPassword { get; set; }

        public string Code { get; set; }
    }
}
