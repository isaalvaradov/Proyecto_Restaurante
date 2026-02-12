using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Proyecto_Restaurante.Data
{
    public class RestauranteDbContextFactory
        : IDesignTimeDbContextFactory<RestauranteDbContext>
    {
        public RestauranteDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RestauranteDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=.;Database=ProyectoRestauranteDB;Trusted_Connection=True;TrustServerCertificate=True"
            );

            return new RestauranteDbContext(optionsBuilder.Options);
        }
    }
}
