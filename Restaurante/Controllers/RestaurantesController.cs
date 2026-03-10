using Microsoft.AspNetCore.Mvc;
using Proyecto_Restaurante.Models;
using Proyecto_Restaurante.Services.Interfaces;

namespace Proyecto_Restaurante.Controllers
{
    public class RestaurantesController : Controller
    {
        private readonly IRestauranteService _service;

        public RestaurantesController(IRestauranteService service)
        {
            _service = service;
        }


        public IActionResult Index()
        {
            var restaurantes = _service.ObtenerTodos();
            return View(restaurantes);
        }


        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Restaurante restaurante)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
                return RedirectToAction("Index");

            if (!ModelState.IsValid)
                return View(restaurante);

            _service.Crear(restaurante);
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
                return RedirectToAction("Index");

            var restaurante = _service.ObtenerPorId(id);

            if (restaurante == null)
                return NotFound();

            return View(restaurante);
        }

        [HttpPost]
        public IActionResult Edit(Restaurante restaurante)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
                return RedirectToAction("Index");

            if (!ModelState.IsValid)
                return View(restaurante);

            _service.Editar(restaurante);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
                return RedirectToAction("Index");

            var restaurante = _service.ObtenerPorId(id);

            if (restaurante == null)
                return NotFound();

            return View(restaurante);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
                return RedirectToAction("Index");

            _service.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}