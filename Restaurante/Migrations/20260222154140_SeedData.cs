using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Proyecto_Restaurante.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurantes",
                columns: table => new
                {
                    RestauranteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoCocina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurantes", x => x.RestauranteId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Platillos",
                columns: table => new
                {
                    PlatilloId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImagenUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestauranteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platillos", x => x.PlatilloId);
                    table.ForeignKey(
                        name: "FK_Platillos_Restaurantes_RestauranteId",
                        column: x => x.RestauranteId,
                        principalTable: "Restaurantes",
                        principalColumn: "RestauranteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Restaurantes",
                columns: new[] { "RestauranteId", "Descripcion", "Nombre", "TipoCocina" },
                values: new object[,]
                {
                    { 1, "Restaurante de comida costarricense tradicional", "La Sazón Tica", "Comida Típica" },
                    { 2, "Auténtica pizza y pasta italiana artesanal", "Pizza Napoli", "Italiana" },
                    { 3, "Las mejores hamburguesas artesanales del país", "Burger House", "Americana" }
                });

            migrationBuilder.InsertData(
                table: "Platillos",
                columns: new[] { "PlatilloId", "Descripcion", "ImagenUrl", "Nombre", "Precio", "RestauranteId" },
                values: new object[,]
                {
                    { 1, "Arroz, frijoles, ensalada, plátano maduro y pollo al horno.", "https://images.unsplash.com/photo-1512058564366-18510be2db19?w=400", "Casado con Pollo", 3500.00m, 1 },
                    { 2, "Tradicional sopa costarricense con res, yuca, plátano y elote.", "https://images.unsplash.com/photo-1547592180-85f173990554?w=400", "Olla de Carne", 4200.00m, 1 },
                    { 3, "Arroz con frijoles, culantro y chile dulce, servido con natilla.", "https://images.unsplash.com/photo-1565299507177-b0ac66763828?w=400", "Gallo Pinto", 2000.00m, 1 },
                    { 4, "Salsa de tomate, mozzarella fresca y albahaca sobre masa artesanal.", "https://images.unsplash.com/photo-1574071318508-1cdbab80d002?w=400", "Pizza Margherita", 6500.00m, 2 },
                    { 5, "Pasta con huevo, queso pecorino, tocineta y pimienta negra.", "https://images.unsplash.com/photo-1612874742237-6526221588e3?w=400", "Pasta Carbonara", 5800.00m, 2 },
                    { 6, "Capas de pasta, carne molida, bechamel y queso gratinado.", "https://images.unsplash.com/photo-1574894709920-11b28e7367e3?w=400", "Lasaña Boloñesa", 6200.00m, 2 },
                    { 7, "Carne de res 180g, lechuga, tomate, queso cheddar y pan brioche.", "https://images.unsplash.com/photo-1568901346375-23c9450c58cd?w=400", "Hamburguesa Clásica", 4800.00m, 3 },
                    { 8, "Papas crujientes con sal marina, servidas con salsa especial.", "https://images.unsplash.com/photo-1573080496219-bb080dd4f877?w=400", "Papas Fritas", 1800.00m, 3 },
                    { 9, "Alitas de pollo bañadas en salsa BBQ ahumada, con vegetales.", "https://images.unsplash.com/photo-1567620832903-9fc6debc209f?w=400", "Alitas BBQ", 4500.00m, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Platillos_RestauranteId",
                table: "Platillos",
                column: "RestauranteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Platillos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Restaurantes");
        }
    }
}
