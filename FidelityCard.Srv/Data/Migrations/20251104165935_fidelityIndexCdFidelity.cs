using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelityCard.Srv.Data.Migrations
{
    /// <inheritdoc />
    public partial class fidelityIndexCdFidelity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Fidelity_CdFidelity",
                table: "Fidelity",
                column: "CdFidelity",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Fidelity_CdFidelity",
                table: "Fidelity");
        }
    }
}
