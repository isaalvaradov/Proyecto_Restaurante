using Proyecto_Restaurante.Models;

namespace Proyecto_Restaurante.Services.Interfaces
{
    public interface IRestauranteService
    {
        List<Restaurante> ObtenerTodos();
        Restaurante? ObtenerPorId(int id);
        void Crear(Restaurante restaurante);
        void Editar(Restaurante restaurante);
        void Eliminar(int id);
    }
}
