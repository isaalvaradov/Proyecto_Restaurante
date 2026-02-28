using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Proyecto_Restaurante.Models;
using Proyecto_Restaurante.Services.Interfaces;
using Proyecto_Restaurante.ViewModels;

namespace Proyecto_Restaurante.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            if (model.Captcha != "5")
            {
                ModelState.AddModelError("", "Captcha incorrecto");
                return View(model);
            }

            var usuario = _authService.Login(model.Correo, model.Password);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Credenciales incorrectas");
                return View(model);
            }


            HttpContext.Session.SetString("Rol", usuario.Rol);
            HttpContext.Session.SetString("Usuario", usuario.Correo);

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            if (model.Captcha != "7")
            {
                ModelState.AddModelError("", "Captcha incorrecto");
                return View(model);
            }

            var usuario = new Usuario
            {
                Nombre = model.Nombre,
                Correo = model.Correo,
                Password = model.Password,
                Rol = "Comprador" 
            };

            _authService.Register(usuario);

            return RedirectToAction("Login");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
