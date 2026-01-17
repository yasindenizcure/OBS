using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DenemeDers.Migrations
{
    /// <inheritdoc />
    public partial class OgrenciEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OgretimGorevlileri_AppUsers_AppUserId",
                table: "OgretimGorevlileri");

            migrationBuilder.DropColumn(
                name: "Bolum",
                table: "Ogrenciler");

            migrationBuilder.AddColumn<int>(
                name: "BolumId",
                table: "Ogrenciler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_BolumId",
                table: "Ogrenciler",
                column: "BolumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrenciler_Bolumler_BolumId",
                table: "Ogrenciler",
                column: "BolumId",
                principalTable: "Bolumler",
                principalColumn: "BolumId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OgretimGorevlileri_AppUsers_AppUserId",
                table: "OgretimGorevlileri",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenciler_Bolumler_BolumId",
                table: "Ogrenciler");

            migrationBuilder.DropForeignKey(
                name: "FK_OgretimGorevlileri_AppUsers_AppUserId",
                table: "OgretimGorevlileri");

            migrationBuilder.DropIndex(
                name: "IX_Ogrenciler_BolumId",
                table: "Ogrenciler");

            migrationBuilder.DropColumn(
                name: "BolumId",
                table: "Ogrenciler");

            migrationBuilder.AddColumn<string>(
                name: "Bolum",
                table: "Ogrenciler",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OgretimGorevlileri_AppUsers_AppUserId",
                table: "OgretimGorevlileri",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
