using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaMud.Migrations
{
    /// <inheritdoc />
    public partial class AnadirEstadoPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Pedidos");
        }
    }
}
