using System.ComponentModel.DataAnnotations;

namespace Proyecto_Restaurante.Models
{
    public class Restaurante
    {
        [Key]
        public int RestauranteId { get; set; }

        public string Nombre { get; set; }
        public string TipoCocina { get; set; }
        public string Descripcion { get; set; }
    }
}
