using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Restaurante.Data;
using Proyecto_Restaurante.Models;

namespace Proyecto_Restaurante.Controllers
{
    public class PlatilloAdminController : Controller
    {
        private readonly RestauranteDbContext _context;

        public PlatilloAdminController(RestauranteDbContext context)
        {
            _context = context;
        }

    
        public async Task<IActionResult> Index()
        {
            var restauranteDbContext = _context.Platillos.Include(p => p.Restaurante);
            return View(await restauranteDbContext.ToListAsync());
        }

    
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platillo = await _context.Platillos
                .Include(p => p.Restaurante)
                .FirstOrDefaultAsync(m => m.PlatilloId == id);
            if (platillo == null)
            {
                return NotFound();
            }

            return View(platillo);
        }

      
        public IActionResult Create()
        {
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "RestauranteId", "RestauranteId");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlatilloId,Nombre,Descripcion,Precio,ImagenUrl,RestauranteId")] Platillo platillo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(platillo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "RestauranteId", "RestauranteId", platillo.RestauranteId);
            return View(platillo);
        }

    
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platillo = await _context.Platillos.FindAsync(id);
            if (platillo == null)
            {
                return NotFound();
            }
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "RestauranteId", "RestauranteId", platillo.RestauranteId);
            return View(platillo);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlatilloId,Nombre,Descripcion,Precio,ImagenUrl,RestauranteId")] Platillo platillo)
        {
            if (id != platillo.PlatilloId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(platillo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatilloExists(platillo.PlatilloId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "RestauranteId", "RestauranteId", platillo.RestauranteId);
            return View(platillo);
        }

  
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platillo = await _context.Platillos
                .Include(p => p.Restaurante)
                .FirstOrDefaultAsync(m => m.PlatilloId == id);
            if (platillo == null)
            {
                return NotFound();
            }

            return View(platillo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var platillo = await _context.Platillos.FindAsync(id);
            if (platillo != null)
            {
                _context.Platillos.Remove(platillo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlatilloExists(int id)
        {
            return _context.Platillos.Any(e => e.PlatilloId == id);
        }
    }
}
