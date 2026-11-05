using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerreteriaValdivia.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarModeloProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)   
        {
            migrationBuilder.AddColumn<int>(
                name: "CantidadVendida",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadVendida",
                table: "Productos");
        }
    }
}
