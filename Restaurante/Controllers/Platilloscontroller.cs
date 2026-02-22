using Microsoft.AspNetCore.Mvc;
using Proyecto_Restaurante.Data;
using Proyecto_Restaurante.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Proyecto_Restaurante.Controllers
{
    public class PlatillosController : Controller
    {
        private readonly RestauranteDbContext _context;
        private const string CarritoKey = "Carrito";

        public PlatillosController(RestauranteDbContext context)
        {
            _context = context;
        }

        // GET: Platillos/Index/5  (platillos de un restaurante)
        public IActionResult Index(int restauranteId)
        {
            var restaurante = _context.Restaurantes.Find(restauranteId);
            if (restaurante == null) return NotFound();

            var platillos = _context.Platillos
                .Where(p => p.RestauranteId == restauranteId)
                .ToList();

            ViewBag.Restaurante = restaurante;
            return View(platillos);
        }

        // GET: Platillos/Detalle/5
        public IActionResult Detalle(int id)
        {
            var platillo = _context.Platillos
                .Include(p => p.Restaurante)
                .FirstOrDefault(p => p.PlatilloId == id);

            if (platillo == null) return NotFound();

            return View(platillo);
        }

        // POST: Platillos/AgregarAlCarrito
        [HttpPost]
        public IActionResult AgregarAlCarrito(int platilloId, int restauranteId)
        {
            var platillo = _context.Platillos.Find(platilloId);
            if (platillo == null) return NotFound();

            var carrito = ObtenerCarrito();

            var item = carrito.FirstOrDefault(c => c.PlatilloId == platilloId);
            if (item != null)
            {
                item.Cantidad++;
            }
            else
            {
                carrito.Add(new CarritoItem
                {
                    PlatilloId = platillo.PlatilloId,
                    Nombre = platillo.Nombre,
                    Descripcion = platillo.Descripcion,
                    Precio = platillo.Precio,
                    Cantidad = 1,
                    ImagenUrl = platillo.ImagenUrl
                });
            }

            GuardarCarrito(carrito);
            return RedirectToAction("Index", new { restauranteId });
        }

        // POST: Platillos/EliminarDelCarrito
        [HttpPost]
        public IActionResult EliminarDelCarrito(int platilloId)
        {
            var carrito = ObtenerCarrito();
            var item = carrito.FirstOrDefault(c => c.PlatilloId == platilloId);
            if (item != null) carrito.Remove(item);
            GuardarCarrito(carrito);
            return RedirectToAction("Carrito");
        }

        // GET: Platillos/Carrito
        public IActionResult Carrito()
        {
            var carrito = ObtenerCarrito();
            return View(carrito);
        }

        private List<CarritoItem> ObtenerCarrito()
        {
            var json = HttpContext.Session.GetString(CarritoKey);
            if (string.IsNullOrEmpty(json)) return new List<CarritoItem>();
            return JsonSerializer.Deserialize<List<CarritoItem>>(json) ?? new List<CarritoItem>();
        }

        private void GuardarCarrito(List<CarritoItem> carrito)
        {
            HttpContext.Session.SetString(CarritoKey, JsonSerializer.Serialize(carrito));
        }
    }
}