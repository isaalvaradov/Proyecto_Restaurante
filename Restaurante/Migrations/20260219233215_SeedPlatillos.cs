using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Proyecto_Restaurante.Migrations
{
    /// <inheritdoc />
    public partial class SeedPlatillos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Restaurantes",
                columns: new[] { "RestauranteId", "Descripcion", "Nombre", "TipoCocina" },
                values: new object[] { 1, "El mejor restaurante de la ciudad", "Restaurante Central", "Variada" });

            migrationBuilder.InsertData(
                table: "Platillos",
                columns: new[] { "PlatilloId", "Categoria", "Descripcion", "ImagenUrl", "Nombre", "Precio", "RestauranteId" },
                values: new object[,]
                {
                    { 1, "Pizzas", "Pizza tradicional con tomate, mozzarella y albahaca", "https://images.unsplash.com/photo-1574071318508-1cdbab80d002?w=400", "Pizza Margarita", 120.00m, 1 },
                    { 2, "Hamburguesas", "Carne de res, lechuga, tomate, cebolla y queso cheddar", "https://images.unsplash.com/photo-1568901346375-23c9450c58cd?w=400", "Hamburguesa Clásica", 95.00m, 1 },
                    { 3, "Ensaladas", "Lechuga romana, aderezo césar, crutones y parmesano", "https://images.unsplash.com/photo-1546793665-c74683f339c1?w=400", "Ensalada César", 80.00m, 1 },
                    { 4, "Pastas", "Fettuccine en salsa cremosa de queso parmesano", "https://images.unsplash.com/photo-1645112411341-6c4fd023714a?w=400", "Pasta Alfredo", 110.00m, 1 },
                    { 5, "Mexicano", "3 tacos con carne al pastor, piña, cilantro y cebolla", "https://images.unsplash.com/photo-1565299585323-38d6b0865b47?w=400", "Tacos al Pastor", 75.00m, 1 },
                    { 6, "Japonés", "8 piezas de california roll con aguacate y surimi", "https://images.unsplash.com/photo-1579584425555-c3ce17fd4351?w=400", "Sushi Roll", 130.00m, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Restaurantes",
                keyColumn: "RestauranteId",
                keyValue: 1);
        }
    }
}
