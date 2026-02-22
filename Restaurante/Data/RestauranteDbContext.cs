using Microsoft.EntityFrameworkCore;
using Proyecto_Restaurante.Models;

namespace Proyecto_Restaurante.Data
{
    public class RestauranteDbContext : DbContext
    {
        public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Platillo> Platillos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Restaurantes
            modelBuilder.Entity<Restaurante>().HasData(
                new Restaurante { RestauranteId = 1, Nombre = "La Sazón Tica", TipoCocina = "Comida Típica", Descripcion = "Restaurante de comida costarricense tradicional" },
                new Restaurante { RestauranteId = 2, Nombre = "Pizza Napoli", TipoCocina = "Italiana", Descripcion = "Auténtica pizza y pasta italiana artesanal" },
                new Restaurante { RestauranteId = 3, Nombre = "Burger House", TipoCocina = "Americana", Descripcion = "Las mejores hamburguesas artesanales del país" }
            );

            // Seed Platillos
            modelBuilder.Entity<Platillo>().HasData(
                // La Sazón Tica
                new Platillo { PlatilloId = 1, Nombre = "Casado con Pollo", Descripcion = "Arroz, frijoles, ensalada, plátano maduro y pollo al horno.", Precio = 3500.00m, ImagenUrl = "https://images.unsplash.com/photo-1512058564366-18510be2db19?w=400", RestauranteId = 1 },
                new Platillo { PlatilloId = 2, Nombre = "Olla de Carne", Descripcion = "Tradicional sopa costarricense con res, yuca, plátano y elote.", Precio = 4200.00m, ImagenUrl = "https://images.unsplash.com/photo-1547592180-85f173990554?w=400", RestauranteId = 1 },
                new Platillo { PlatilloId = 3, Nombre = "Gallo Pinto", Descripcion = "Arroz con frijoles, culantro y chile dulce, servido con natilla.", Precio = 2000.00m, ImagenUrl = "https://images.unsplash.com/photo-1565299507177-b0ac66763828?w=400", RestauranteId = 1 },

                // Pizza Napoli
                new Platillo { PlatilloId = 4, Nombre = "Pizza Margherita", Descripcion = "Salsa de tomate, mozzarella fresca y albahaca sobre masa artesanal.", Precio = 6500.00m, ImagenUrl = "https://images.unsplash.com/photo-1574071318508-1cdbab80d002?w=400", RestauranteId = 2 },
                new Platillo { PlatilloId = 5, Nombre = "Pasta Carbonara", Descripcion = "Pasta con huevo, queso pecorino, tocineta y pimienta negra.", Precio = 5800.00m, ImagenUrl = "https://images.unsplash.com/photo-1612874742237-6526221588e3?w=400", RestauranteId = 2 },
                new Platillo { PlatilloId = 6, Nombre = "Lasaña Boloñesa", Descripcion = "Capas de pasta, carne molida, bechamel y queso gratinado.", Precio = 6200.00m, ImagenUrl = "https://images.unsplash.com/photo-1574894709920-11b28e7367e3?w=400", RestauranteId = 2 },

                // Burger House
                new Platillo { PlatilloId = 7, Nombre = "Hamburguesa Clásica", Descripcion = "Carne de res 180g, lechuga, tomate, queso cheddar y pan brioche.", Precio = 4800.00m, ImagenUrl = "https://images.unsplash.com/photo-1568901346375-23c9450c58cd?w=400", RestauranteId = 3 },
                new Platillo { PlatilloId = 8, Nombre = "Papas Fritas", Descripcion = "Papas crujientes con sal marina, servidas con salsa especial.", Precio = 1800.00m, ImagenUrl = "https://images.unsplash.com/photo-1573080496219-bb080dd4f877?w=400", RestauranteId = 3 },
                new Platillo { PlatilloId = 9, Nombre = "Alitas BBQ", Descripcion = "Alitas de pollo bañadas en salsa BBQ ahumada, con vegetales.", Precio = 4500.00m, ImagenUrl = "https://images.unsplash.com/photo-1567620832903-9fc6debc209f?w=400", RestauranteId = 3 }
            );
        }
    }
}