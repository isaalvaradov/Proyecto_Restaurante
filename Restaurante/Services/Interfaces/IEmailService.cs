using System.Threading.Tasks;

namespace Proyecto_Restaurante.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
