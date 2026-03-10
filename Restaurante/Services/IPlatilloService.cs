using Proyecto_Restaurante.Models;

namespace Proyecto_Restaurante.Services
{
    public interface IPlatilloService
    {
        List<Platillo> ObtenerTodos();
        Platillo ObtenerPorId(int id);
        void Crear(Platillo platillo);
        void Editar(Platillo platillo);
        void Eliminar(int id);
    }
}