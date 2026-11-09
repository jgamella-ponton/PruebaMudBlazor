using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaMud.Migrations
{
    /// <inheritdoc />
    public partial class AnadirCalcResultados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Clientes_ClienteId",
                table: "Requests");

            migrationBuilder.CreateTable(
                name: "CalcResultados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreArchivo = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: true),
                    RequestId = table.Column<long>(type: "bigint", nullable: true),
                    PlateCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RespuestaJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalcResultados", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Clientes_ClienteId",
                table: "Requests",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Clientes_ClienteId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "CalcResultados");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Clientes_ClienteId",
                table: "Requests",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
