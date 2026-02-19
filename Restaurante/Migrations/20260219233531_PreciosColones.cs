using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Restaurante.Migrations
{
    /// <inheritdoc />
    public partial class PreciosColones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 1,
                column: "Precio",
                value: 6500m);

            migrationBuilder.UpdateData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 2,
                column: "Precio",
                value: 5200m);

            migrationBuilder.UpdateData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 3,
                column: "Precio",
                value: 4800m);

            migrationBuilder.UpdateData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 4,
                column: "Precio",
                value: 5800m);

            migrationBuilder.UpdateData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 5,
                column: "Precio",
                value: 4200m);

            migrationBuilder.UpdateData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 6,
                column: "Precio",
                value: 7500m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 1,
                column: "Precio",
                value: 120.00m);

            migrationBuilder.UpdateData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 2,
                column: "Precio",
                value: 95.00m);

            migrationBuilder.UpdateData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 3,
                column: "Precio",
                value: 80.00m);

            migrationBuilder.UpdateData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 4,
                column: "Precio",
                value: 110.00m);

            migrationBuilder.UpdateData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 5,
                column: "Precio",
                value: 75.00m);

            migrationBuilder.UpdateData(
                table: "Platillos",
                keyColumn: "PlatilloId",
                keyValue: 6,
                column: "Precio",
                value: 130.00m);
        }
    }
}
