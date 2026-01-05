using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DenemeDers.Migrations
{
    /// <inheritdoc />
    public partial class MigUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dersler_Bolumler_BolumId",
                table: "Dersler");

            migrationBuilder.AddForeignKey(
                name: "FK_Dersler_Bolumler_BolumId",
                table: "Dersler",
                column: "BolumId",
                principalTable: "Bolumler",
                principalColumn: "BolumId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dersler_Bolumler_BolumId",
                table: "Dersler");

            migrationBuilder.AddForeignKey(
                name: "FK_Dersler_Bolumler_BolumId",
                table: "Dersler",
                column: "BolumId",
                principalTable: "Bolumler",
                principalColumn: "BolumId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
