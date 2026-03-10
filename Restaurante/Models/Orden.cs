using System.ComponentModel.DataAnnotations;

namespace Proyecto_Restaurante.Models
{
    public class Orden
    {
        [Key]
        public int OrdenId { get; set; }
        public DateTime FechaCompra { get; set; }
        public string CompradorCorreo { get; set; }
        public decimal Total { get; set; }

        public List<OrdenItem> Items { get; set; } = new List<OrdenItem>();
    }
}
