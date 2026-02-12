namespace Proyecto_Restaurante.Models
{
    public class Platillo
    {
        public int PlatilloId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int RestauranteId { get; set; }

        public Restaurante Restaurante { get; set; }
    }
}
