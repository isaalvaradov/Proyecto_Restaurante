using Microsoft.EntityFrameworkCore;
using Proyecto_Restaurante.Models;
using System.Collections.Generic;

namespace Proyecto_Restaurante.Data
{
    public class RestauranteDbContext : DbContext
    {
        public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Platillo> Platillos { get; set; }

    }
}
