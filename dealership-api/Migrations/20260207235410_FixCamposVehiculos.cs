using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dealership_api.Migrations
{
    /// <inheritdoc />
    public partial class FixCamposVehiculos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disponible",
                table: "Vehiculos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disponible",
                table: "Vehiculos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
