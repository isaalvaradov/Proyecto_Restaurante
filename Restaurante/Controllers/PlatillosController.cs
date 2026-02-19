using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Proyecto_Restaurante.Data;
using Proyecto_Restaurante.Models;

namespace Proyecto_Restaurante.Controllers
{
    public class PlatillosController : Controller
    {
        private readonly RestauranteDbContext _context;

        public PlatillosController(RestauranteDbContext context)
        {
            _context = context;
        }

        // GET: Platillos - Menú público
        public IActionResult Index()
        {
            var platillos = _context.Platillos.Include(p => p.Restaurante).ToList();
            return View(platillos);
        }

        // GET: Platillos/Detalle/5
        public IActionResult Detalle(int id)
        {
            var platillo = _context.Platillos.Include(p => p.Restaurante)
                                             .FirstOrDefault(p => p.PlatilloId == id);
            if (platillo == null) return NotFound();
            return View(platillo);
        }

        // GET: Platillos/Create (solo Administrador)
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
                return RedirectToAction("Login", "Account");

            ViewBag.Restaurantes = _context.Restaurantes.ToList();
            return View();
        }

        // POST: Platillos/Create
        [HttpPost]
        public IActionResult Create(Platillo platillo)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                ViewBag.Restaurantes = _context.Restaurantes.ToList();
                return View(platillo);
            }

            _context.Platillos.Add(platillo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Platillos/Edit/5 (solo Administrador)
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
                return RedirectToAction("Login", "Account");

            var platillo = _context.Platillos.Find(id);
            if (platillo == null) return NotFound();

            ViewBag.Restaurantes = _context.Restaurantes.ToList();
            return View(platillo);
        }

        // POST: Platillos/Edit
        [HttpPost]
        public IActionResult Edit(Platillo platillo)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                ViewBag.Restaurantes = _context.Restaurantes.ToList();
                return View(platillo);
            }

            _context.Platillos.Update(platillo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Platillos/Delete/5 (solo Administrador)
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
                return RedirectToAction("Login", "Account");

            var platillo = _context.Platillos.Find(id);
            if (platillo == null) return NotFound();
            return View(platillo);
        }

        // POST: Platillos/Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
                return RedirectToAction("Login", "Account");

            var platillo = _context.Platillos.Find(id);
            if (platillo != null)
            {
                _context.Platillos.Remove(platillo);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // ── Carrito ──────────────────────────────────────────

        // POST: Platillos/AgregarAlCarrito
        [HttpPost]
        public IActionResult AgregarAlCarrito(int id)
        {
            var platillo = _context.Platillos.Find(id);
            if (platillo == null) return NotFound();

            var carrito = ObtenerCarrito();
            var item = carrito.FirstOrDefault(c => c.Platillo.PlatilloId == id);

            if (item != null)
                item.Cantidad++;
            else
                carrito.Add(new CarritoItem { Platillo = platillo, Cantidad = 1 });

            GuardarCarrito(carrito);
            TempData["Mensaje"] = $"✅ {platillo.Nombre} agregado al carrito";
            return RedirectToAction("Index");
        }

        // GET: Platillos/Carrito
        public IActionResult Carrito()
        {
            var carrito = ObtenerCarrito();
            ViewBag.Total = carrito.Sum(c => c.Subtotal);
            return View(carrito);
        }

        // POST: Platillos/EliminarDelCarrito
        [HttpPost]
        public IActionResult EliminarDelCarrito(int id)
        {
            var carrito = ObtenerCarrito();
            var item = carrito.FirstOrDefault(c => c.Platillo.PlatilloId == id);
            if (item != null)
            {
                carrito.Remove(item);
                GuardarCarrito(carrito);
                TempData["Mensaje"] = "Producto eliminado del carrito";
            }
            return RedirectToAction("Carrito");
        }

        // ── Helpers de sesión ─────────────────────────────────

        private List<CarritoItem> ObtenerCarrito()
        {
            var json = HttpContext.Session.GetString("Carrito");
            if (string.IsNullOrEmpty(json)) return new List<CarritoItem>();
            return JsonConvert.DeserializeObject<List<CarritoItem>>(json) ?? new List<CarritoItem>();
        }

        private void GuardarCarrito(List<CarritoItem> carrito)
        {
            HttpContext.Session.SetString("Carrito", JsonConvert.SerializeObject(carrito));
        }
    }
}