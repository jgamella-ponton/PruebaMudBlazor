using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaMud.Migrations
{
    /// <inheritdoc />
    public partial class AnadirRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DosisMPA = table.Column<int>(type: "int", nullable: false),
                    DosisMTP = table.Column<int>(type: "int", nullable: false),
                    DosisEVE = table.Column<int>(type: "int", nullable: false),
                    DosisTAC = table.Column<int>(type: "int", nullable: false),
                    MPA = table.Column<bool>(type: "bit", nullable: false),
                    MTP = table.Column<bool>(type: "bit", nullable: false),
                    EVE = table.Column<bool>(type: "bit", nullable: false),
                    TAC = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ClienteId",
                table: "Requests",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
