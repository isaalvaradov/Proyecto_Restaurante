using Proyecto_Restaurante.Data;
using Proyecto_Restaurante.Models;
using Proyecto_Restaurante.Services.Interfaces;

namespace Proyecto_Restaurante.Services
{
    public class RestauranteService : IRestauranteService
    {
        private readonly RestauranteDbContext _context;

        public RestauranteService(RestauranteDbContext context)
        {
            _context = context;
        }

        public List<Restaurante> ObtenerTodos()
        {
            return _context.Restaurantes.ToList();
        }

        public Restaurante? ObtenerPorId(int id)
        {
            return _context.Restaurantes.Find(id);
        }

        public void Crear(Restaurante restaurante)
        {
            _context.Restaurantes.Add(restaurante);
            _context.SaveChanges();
        }

        public void Editar(Restaurante restaurante)
        {
            _context.Restaurantes.Update(restaurante);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var restaurante = ObtenerPorId(id);
            if (restaurante != null)
            {
                _context.Restaurantes.Remove(restaurante);
                _context.SaveChanges();
            }
        }
    }
}
