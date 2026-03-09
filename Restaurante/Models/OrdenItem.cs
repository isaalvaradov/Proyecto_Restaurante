namespace Proyecto_Restaurante.Models
{
    public class OrdenItem
    {
        public int OrdenItemId { get; set; }
        public int PlatilloId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
