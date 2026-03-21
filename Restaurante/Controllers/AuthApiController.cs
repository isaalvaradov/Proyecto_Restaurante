using Microsoft.AspNetCore.Mvc;
using Proyecto_Restaurante.ViewModels;
using Proyecto_Restaurante.Services.Interfaces;
using Proyecto_Restaurante.Models;

namespace Proyecto_Restaurante.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthApiController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginApiViewModel model)
        {
            var usuario = _authService.Login(model.Correo!, model.Password!);

            if (usuario == null)
            {
                return Unauthorized(new { mensaje = "Credenciales inválidas" });
            }

            return Ok(usuario);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            var usuario = new Usuario
            {
                Nombre = model.Nombre ?? "",
                Correo = model.Correo ?? "",
                Password = model.Password ?? "",
                Rol = "Administrador"
            };

            _authService.Register(usuario);

            return Ok(new { mensaje = "Usuario registrado correctamente" });
        }
    }
}