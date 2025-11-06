using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelityCard.Srv.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fidelity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examples");

            migrationBuilder.CreateTable(
                name: "Fidelity",
                columns: table => new
                {
                    IdFidelity = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CdFidelity = table.Column<string>(type: "varchar(20)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Cognome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    DataNascita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Sesso = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    Indirizzo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Localita = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Cap = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Provincia = table.Column<string>(type: "char(2)", maxLength: 2, nullable: false),
                    Nazione = table.Column<string>(type: "char(2)", maxLength: 2, nullable: false),
                    Cellulare = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fidelity", x => x.IdFidelity);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fidelity");

            migrationBuilder.CreateTable(
                name: "Examples",
                columns: table => new
                {
                    ExampleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExampleDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExampleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples", x => x.ExampleId);
                });
        }
    }
}
