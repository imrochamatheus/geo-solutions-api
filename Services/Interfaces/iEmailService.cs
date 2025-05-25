using System.Globalization;

namespace GeoSolucoesAPI.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
