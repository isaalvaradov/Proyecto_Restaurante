using System.ComponentModel.DataAnnotations;

namespace Proyecto_Restaurante.Models
{
    public class Platillo
    {
        [Key]
        public int PlatilloId { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        [Required]
        public decimal Precio { get; set; }

        public string ImagenUrl { get; set; } = string.Empty;

        public string Categoria { get; set; } = string.Empty;

        public int RestauranteId { get; set; }
        public Restaurante? Restaurante { get; set; }
    }
}