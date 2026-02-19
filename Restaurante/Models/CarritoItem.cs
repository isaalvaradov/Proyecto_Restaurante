namespace Proyecto_Restaurante.Models
{
    public class CarritoItem
    {
        public Platillo Platillo { get; set; } = new Platillo();
        public int Cantidad { get; set; }
        public decimal Subtotal => Platillo.Precio * Cantidad;
    }
}