using System;
using System.Collections.Generic;

namespace Proyecto_Restaurante.Models
{
    public class Orden
    {
        public int OrdenId { get; set; }
        public DateTime FechaCompra { get; set; }
        public string CompradorCorreo { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public List<OrdenItem> Items { get; set; } = new List<OrdenItem>();
    }
}
