using Microsoft.Extensions.Configuration;
using Proyecto_Restaurante.Services.Interfaces;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SmtpEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var host = _configuration["Smtp:Host"];
            var port = int.TryParse(_configuration["Smtp:Port"], out var p) ? p : 25;
            var user = _configuration["Smtp:User"];
            var pass = _configuration["Smtp:Password"];
            var from = _configuration["Smtp:From"] ?? user;

            using var client = new SmtpClient(host, port)
            {
                EnableSsl = true
            };

            if (!string.IsNullOrEmpty(user))
            {
                client.Credentials = new System.Net.NetworkCredential(user, pass);
            }

            var mail = new MailMessage(from, to, subject, body);
            await client.SendMailAsync(mail);
        }
    }
}
