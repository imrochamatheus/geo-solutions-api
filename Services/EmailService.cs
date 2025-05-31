using GeoSolucoesAPI.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace GeoSolucoesAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailWithAttachmentAsync(string to, string subject, string body, byte[] attachmentBytes, string attachmentName)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config["EmailSettings:From"]));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;

                var builder = new BodyBuilder
                {
                    HtmlBody = body
                };

                builder.Attachments.Add(attachmentName, attachmentBytes, ContentType.Parse("application/pdf"));

                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(
                    _config["EmailSettings:SmtpServer"],
                    int.Parse(_config["EmailSettings:Port"]),
                    SecureSocketOptions.StartTls
                );
                await smtp.AuthenticateAsync("geosolucoes61@gmail.com", "clme etdr bicm cnmc");
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar e-mail com anexo: " + ex.Message);
                throw;
            }
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:From"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body,
                ContentTransferEncoding = ContentEncoding.QuotedPrintable
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                _config["EmailSettings:SmtpServer"],
                int.Parse(_config["EmailSettings:Port"]),
                SecureSocketOptions.StartTls
            );
            await smtp.AuthenticateAsync("geosolucoes61@gmail.com", "clme etdr bicm cnmc");
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception ex)
    {
        Console.WriteLine("Erro ao enviar e-mail: " + ex.Message);
        throw;
    }
}
    }
}
