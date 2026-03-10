using Microsoft.AspNetCore.Mvc;
using Proyecto_Restaurante.Data;
using Proyecto_Restaurante.Models;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Restaurante.Controllers
{
    public class RolesController : Controller
    {
        private readonly RestauranteDbContext _context;

        public RolesController(RestauranteDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Administrador")
            {
                return RedirectToAction("Login", "Account");
            }

            var usuarios = _context.Usuarios.ToList();

                return View(usuarios);
        }

        [HttpPost]
        public IActionResult CambiarRol(int id)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Administrador")
            {
                return RedirectToAction("Login", "Account");
            }

            var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);

            if (usuario != null)
            {
                if (usuario.Rol == "Administrador")
                {
                    usuario.Rol = "Comprador";
                }
                else
                {
                    usuario.Rol = "Administrador";
                }

                _context.SaveChanges();
            }
            return RedirectToAction("Index");

        }
    }
}
