using Microsoft.AspNetCore.Mvc;
using Proyecto_Restaurante.Data;
using Proyecto_Restaurante.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text;
using Proyecto_Restaurante.Services.Interfaces;
using Stripe;
using Stripe.Checkout;
using System.Text.RegularExpressions;

namespace Proyecto_Restaurante.Controllers
{
    public class PlatillosController : Controller
    {
        private readonly RestauranteDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PlatillosController> _logger;
        private const string CarritoKey = "Carrito";

        public PlatillosController(RestauranteDbContext context, IEmailService emailService, IConfiguration configuration, ILogger<PlatillosController> logger)
        {
            _context = context;
            _emailService = emailService;
            _configuration = configuration;
            _logger = logger;
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

        // GET: Platillos/Checkout
        public IActionResult Checkout()
        {
            var carrito = ObtenerCarrito();
            if (carrito == null || !carrito.Any()) return RedirectToAction("Carrito");
            return View(carrito);
        }

        // POST: Platillos/Checkout
        // Disabled: purchases must be completed via Stripe payment flow.
        [HttpPost]
        public Task<IActionResult> CheckoutPost()
        {
            var carrito = ObtenerCarrito();
            if (carrito == null || !carrito.Any()) return Task.FromResult<IActionResult>(RedirectToAction("Carrito"));

            TempData["StripeError"] = "Las compras se completan únicamente tras el pago con tarjeta. Usa 'Pagar con tarjeta (Stripe)'.";
            return Task.FromResult<IActionResult>(RedirectToAction("Checkout"));
        }

        // La acción SendTestEmail fue eliminada: el envío de correo solo ocurre tras el pago confirmado por Stripe (en StripeSuccess).

        public IActionResult CompraExitosa()
        {
            return View();
        }

        // Stripe: crear una sesión de Checkout usando la librería oficial (modo prueba)
        [HttpPost]
        public async Task<IActionResult> CreateStripeSession()
        {
            var carrito = ObtenerCarrito();
            if (carrito == null || !carrito.Any()) return RedirectToAction("Carrito");

            var stripeSecret = (_configuration["Stripe:SecretKey"]
                               ?? Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY"))
                               ?.Trim();

            bool IsProbablyValidStripeKey(string? k) =>
                !string.IsNullOrWhiteSpace(k)
                && (k.StartsWith("sk_test_") || k.StartsWith("sk_live_"))
                && Regex.IsMatch(k, @"^sk_(test|live)_[A-Za-z0-9]{16,}$") // ajustar longitud según necesidades
                && !k.Contains("TU_CLAVE") && !k.Contains("CLAVE_DE_PRUEBA");

            if (!IsProbablyValidStripeKey(stripeSecret))
            {
                TempData["StripeError"] = "Stripe no está configurado correctamente.";
                return RedirectToAction("Checkout");
            }
            StripeConfiguration.ApiKey = stripeSecret;

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",
                SuccessUrl = Url.Action("StripeSuccess", "Platillos", null, Request.Scheme) + "?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = Url.Action("Checkout", "Platillos", null, Request.Scheme),
                LineItems = new List<SessionLineItemOptions>()
            };

            foreach (var item in carrito)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Nombre
                        },
                        UnitAmount = (long)(item.Precio * 100)
                    },
                    Quantity = item.Cantidad
                });
            }

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            if (string.IsNullOrEmpty(session.Url))
            {
                TempData["StripeError"] = "No se pudo crear la sesión de Stripe (sin URL). Revisa logs.";
                return RedirectToAction("Checkout");
            }

            return Redirect(session.Url);
        }

        // Endpoint que Stripe redirige tras pago exitoso (modo test)
        public async Task<IActionResult> StripeSuccess(string? session_id)
        {
            if (string.IsNullOrEmpty(session_id))
            {
                TempData["StripeError"] = "Session id de Stripe no proporcionado.";
                return RedirectToAction("Checkout");
            }

            var secretKey = _configuration["Stripe:SecretKey"];
            StripeConfiguration.ApiKey = secretKey;

            var sessionService = new SessionService();
            var session = await sessionService.GetAsync(session_id);

            if (session == null || session.PaymentStatus != "paid")
            {
                TempData["StripeError"] = "Pago no confirmado por Stripe.";
                return RedirectToAction("Checkout");
            }

            // Crear la orden localmente
            var carrito = ObtenerCarrito();
            var correo = HttpContext.Session.GetString("Usuario") ?? string.Empty;

            var orden = new Models.Orden
            {
                FechaCompra = DateTime.UtcNow,
                CompradorCorreo = correo,
                Total = carrito.Sum(i => i.Precio * i.Cantidad),
                Items = carrito.Select(ci => new Models.OrdenItem
                {
                    PlatilloId = ci.PlatilloId,
                    Nombre = ci.Nombre,
                    Precio = ci.Precio,
                    Cantidad = ci.Cantidad
                }).ToList()
            };

            _context.Ordenes.Add(orden);
            _context.SaveChanges();

            // Enviar correo de confirmación
            var sb = new StringBuilder();
            sb.AppendLine($"Fecha de compra: {orden.FechaCompra:u}");
            sb.AppendLine("Productos adquiridos:");
            foreach (var item in orden.Items)
            {
                sb.AppendLine($"- {item.Nombre} x{item.Cantidad} @ {item.Precio:C} = {(item.Precio * item.Cantidad):C}");
            }
            sb.AppendLine($"Total: {orden.Total:C}");

            if (!string.IsNullOrEmpty(correo))
            {
                await _emailService.SendEmailAsync(correo, "Confirmación de compra", sb.ToString());
            }

            GuardarCarrito(new List<CarritoItem>());

            return RedirectToAction("CompraExitosa");
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