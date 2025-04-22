namespace GeoSolucoesAPI.Models.Base
{
    public class UserDbo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // pode ser ocultado dependendo do uso
        public string Cell { get; set; }
        public UserType UserType { get; set; }
    }
}
