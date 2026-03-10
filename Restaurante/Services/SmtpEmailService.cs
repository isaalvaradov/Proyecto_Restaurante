using System.Net;
using System.Net.Mail;
using Proyecto_Restaurante.Services.Interfaces;

namespace Proyecto_Restaurante.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SmtpEmailService> _logger;

        public SmtpEmailService(IConfiguration configuration, ILogger<SmtpEmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var host = _configuration["Smtp:Host"];
            var portStr = _configuration["Smtp:Port"];
            var user = _configuration["Smtp:User"];
            var pass = _configuration["Smtp:Password"];
            var from = _configuration["Smtp:From"] ?? user;

            _logger.LogInformation("Preparando envío de correo a {to} usando host {host}", to, host);

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
            {
                _logger.LogWarning("Configuración SMTP incompleta. host='{host}' from='{from}' to='{to}'", host, from, to);
                return;
            }

            // Limpieza básica de la contraseña (en caso de que el usuario pegue con espacios)
            if (!string.IsNullOrEmpty(pass))
            {
                var trimmed = pass.Replace(" ", "");
                if (trimmed.Length > 0) pass = trimmed;
            }

            if (!int.TryParse(portStr, out var port)) port = 587;

            try
            {
                using var client = new SmtpClient(host, port)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Timeout = 10000
                };

                if (!string.IsNullOrEmpty(user))
                {
                    client.Credentials = new NetworkCredential(user, pass);
                }

                var mail = new MailMessage(from, to, subject, body) { IsBodyHtml = false };

                await client.SendMailAsync(mail);
                _logger.LogInformation("Correo enviado correctamente a {to}", to);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enviando correo a {to}", to);
                // No propagar para no romper el flujo de compra; el error queda en logs.
            }
        }
    }
}
