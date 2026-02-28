using Microsoft.AspNetCore.Mvc;
using Proyecto_Restaurante.Models;
using Proyecto_Restaurante.Services;

namespace Proyecto_Restaurante.Controllers
{
    public class PlatillosAdminController : Controller
    {
        private readonly IPlatilloService _service;

        public PlatillosAdminController(IPlatilloService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var platillos = _service.ObtenerTodos();
            return View(platillos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Platillo platillo)
        {
            if (ModelState.IsValid)
            {
                _service.Crear(platillo);
                return RedirectToAction(nameof(Index));
            }
            return View(platillo);
        }

        public IActionResult Edit(int id)
        {
            var platillo = _service.ObtenerPorId(id);
            if (platillo == null) return NotFound();
            return View(platillo);
        }

        [HttpPost]
        public IActionResult Edit(Platillo platillo)
        {
            if (ModelState.IsValid)
            {
                _service.Editar(platillo);
                return RedirectToAction(nameof(Index));
            }
            return View(platillo);
        }

        public IActionResult Delete(int id)
        {
            var platillo = _service.ObtenerPorId(id);
            if (platillo == null) return NotFound();
            return View(platillo);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}