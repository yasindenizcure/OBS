using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DenemeDers.Migrations
{
    /// <inheritdoc />
    public partial class OgrenciNoMigUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OgrNo",
                table: "Ogrenciler",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_OgrNo",
                table: "Ogrenciler",
                column: "OgrNo",
                unique: true,
                filter: "[OgrNo] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ogrenciler_OgrNo",
                table: "Ogrenciler");

            migrationBuilder.AlterColumn<string>(
                name: "OgrNo",
                table: "Ogrenciler",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
