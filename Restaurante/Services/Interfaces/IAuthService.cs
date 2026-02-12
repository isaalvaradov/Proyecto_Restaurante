using Proyecto_Restaurante.Models;

namespace Proyecto_Restaurante.Services.Interfaces
{
    public interface IAuthService
    {
        Usuario? Login(string correo, string password);

        void Register(Usuario usuario);
    }
}
