using Proyecto_Restaurante.Data;
using Proyecto_Restaurante.Models;
using Proyecto_Restaurante.Services.Interfaces;

namespace Proyecto_Restaurante.Services
{
    public class AuthService : IAuthService
    {
        private readonly RestauranteDbContext _context;

        public AuthService(RestauranteDbContext context)
        {
            _context = context;
        }

        public Usuario? Login(string correo, string password)
        {
            return _context.Usuarios
                .FirstOrDefault(u => u.Correo == correo && u.Password == password);
        }

        public void Register(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }
    }
}
