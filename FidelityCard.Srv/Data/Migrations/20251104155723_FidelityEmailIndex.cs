using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelityCard.Srv.Data.Migrations
{
    /// <inheritdoc />
    public partial class FidelityEmailIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Fidelity_Email",
                table: "Fidelity",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Fidelity_Email",
                table: "Fidelity");
        }
    }
}
