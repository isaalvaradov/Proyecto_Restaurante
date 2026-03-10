using Proyecto_Restaurante.Data;
using Proyecto_Restaurante.Models;

namespace Proyecto_Restaurante.Services
{
    public class PlatilloService : IPlatilloService
    {
        private readonly RestauranteDbContext _context;

        public PlatilloService(RestauranteDbContext context)
        {
            _context = context;
        }

        public List<Platillo> ObtenerTodos()
        {
            return _context.Platillos.ToList();
        }

        public Platillo ObtenerPorId(int id)
        {
            return _context.Platillos.Find(id);
        }

        public void Crear(Platillo platillo)
        {
            _context.Platillos.Add(platillo);
            _context.SaveChanges();
        }

        public void Editar(Platillo platillo)
        {
            _context.Platillos.Update(platillo);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var platillo = _context.Platillos.Find(id);
            if (platillo != null)
            {
                _context.Platillos.Remove(platillo);
                _context.SaveChanges();
            }
        }
    }
}