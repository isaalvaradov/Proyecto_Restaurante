using Microsoft.EntityFrameworkCore;
using Proyecto_Restaurante.Models;

namespace Proyecto_Restaurante.Data
{
    public class RestauranteDbContext : DbContext
    {
        public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Restaurante> Restaurantes { get; set; } = null!;
        public DbSet<Platillo> Platillos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Evita truncamiento silencioso en decimales
            modelBuilder.Entity<Platillo>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Restaurante>().HasData(
                new Restaurante
                {
                    RestauranteId = 1,
                    Nombre = "Restaurante Central",
                    TipoCocina = "Variada",
                    Descripcion = "El mejor restaurante de la ciudad"
                }
            );

            modelBuilder.Entity<Platillo>().HasData(
                new Platillo { PlatilloId = 1, Nombre = "Pizza Margarita", Descripcion = "Pizza tradicional con tomate, mozzarella y albahaca", Precio = 6500m, ImagenUrl = "https://images.unsplash.com/photo-1574071318508-1cdbab80d002?w=400", Categoria = "Pizzas", RestauranteId = 1 },
                new Platillo { PlatilloId = 2, Nombre = "Hamburguesa Clásica", Descripcion = "Carne de res, lechuga, tomate, cebolla y queso cheddar", Precio = 5200m, ImagenUrl = "https://images.unsplash.com/photo-1568901346375-23c9450c58cd?w=400", Categoria = "Hamburguesas", RestauranteId = 1 },
                new Platillo { PlatilloId = 3, Nombre = "Ensalada César", Descripcion = "Lechuga romana, aderezo césar, crutones y parmesano", Precio = 4800m, ImagenUrl = "https://images.unsplash.com/photo-1546793665-c74683f339c1?w=400", Categoria = "Ensaladas", RestauranteId = 1 },
                new Platillo { PlatilloId = 4, Nombre = "Pasta Alfredo", Descripcion = "Fettuccine en salsa cremosa de queso parmesano", Precio = 5800m, ImagenUrl = "https://images.unsplash.com/photo-1645112411341-6c4fd023714a?w=400", Categoria = "Pastas", RestauranteId = 1 },
                new Platillo { PlatilloId = 5, Nombre = "Tacos al Pastor", Descripcion = "3 tacos con carne al pastor, piña, cilantro y cebolla", Precio = 4200m, ImagenUrl = "https://images.unsplash.com/photo-1565299585323-38d6b0865b47?w=400", Categoria = "Mexicano", RestauranteId = 1 },
                new Platillo { PlatilloId = 6, Nombre = "Sushi Roll", Descripcion = "8 piezas de california roll con aguacate y surimi", Precio = 7500m, ImagenUrl = "https://images.unsplash.com/photo-1579584425555-c3ce17fd4351?w=400", Categoria = "Japonés", RestauranteId = 1 }
            );
        }
    }
}