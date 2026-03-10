using System.ComponentModel.DataAnnotations;

namespace Proyecto_Restaurante.Models
{
    public class OrdenItem
    {
        [Key]
        public int OrdenItemId { get; set; }
        public int OrdenId { get; set; }

        public int PlatilloId { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public Orden Orden { get; set; }
    }
}
