using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DenemeDers.Migrations
{
    /// <inheritdoc />
    public partial class MigOgrGorevlisiUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bolum",
                table: "OgretimGorevlileri");

            migrationBuilder.AddColumn<int>(
                name: "BolumId",
                table: "OgretimGorevlileri",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OgretimGorevlileri_BolumId",
                table: "OgretimGorevlileri",
                column: "BolumId");

            migrationBuilder.AddForeignKey(
                name: "FK_OgretimGorevlileri_Bolumler_BolumId",
                table: "OgretimGorevlileri",
                column: "BolumId",
                principalTable: "Bolumler",
                principalColumn: "BolumId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OgretimGorevlileri_Bolumler_BolumId",
                table: "OgretimGorevlileri");

            migrationBuilder.DropIndex(
                name: "IX_OgretimGorevlileri_BolumId",
                table: "OgretimGorevlileri");

            migrationBuilder.DropColumn(
                name: "BolumId",
                table: "OgretimGorevlileri");

            migrationBuilder.AddColumn<string>(
                name: "Bolum",
                table: "OgretimGorevlileri",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
