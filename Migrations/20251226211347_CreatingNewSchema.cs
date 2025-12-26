using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DenemeDers.Migrations
{
    /// <inheritdoc />
    public partial class CreatingNewSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.AppUserId);
                });

            migrationBuilder.CreateTable(
                name: "Ogrenciler",
                columns: table => new
                {
                    OgrenciId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yas = table.Column<int>(type: "int", nullable: false),
                    Sinif = table.Column<int>(type: "int", nullable: false),
                    Bolum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OgrNo = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenciler", x => x.OgrenciId);
                    table.ForeignKey(
                        name: "FK_Ogrenciler_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OgretimGorevlileri",
                columns: table => new
                {
                    OgretimGorevlisiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bolum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unvan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgretimGorevlileri", x => x.OgretimGorevlisiId);
                    table.ForeignKey(
                        name: "FK_OgretimGorevlileri_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dersler",
                columns: table => new
                {
                    DersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SinifDuzeyi = table.Column<int>(type: "int", nullable: false),
                    DersTuru = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OgretimGorevlisiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dersler", x => x.DersId);
                    table.ForeignKey(
                        name: "FK_Dersler_OgretimGorevlileri_OgretimGorevlisiId",
                        column: x => x.OgretimGorevlisiId,
                        principalTable: "OgretimGorevlileri",
                        principalColumn: "OgretimGorevlisiId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notlar",
                columns: table => new
                {
                    NotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrenciId = table.Column<int>(type: "int", nullable: false),
                    DersId = table.Column<int>(type: "int", nullable: false),
                    OgretimGorevlisiId = table.Column<int>(type: "int", nullable: false),
                    VizeNotu = table.Column<int>(type: "int", nullable: false),
                    FinalNotu = table.Column<int>(type: "int", nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notlar", x => x.NotId);
                    table.ForeignKey(
                        name: "FK_Notlar_Dersler_DersId",
                        column: x => x.DersId,
                        principalTable: "Dersler",
                        principalColumn: "DersId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notlar_Ogrenciler_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrenciler",
                        principalColumn: "OgrenciId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notlar_OgretimGorevlileri_OgretimGorevlisiId",
                        column: x => x.OgretimGorevlisiId,
                        principalTable: "OgretimGorevlileri",
                        principalColumn: "OgretimGorevlisiId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dersler_OgretimGorevlisiId",
                table: "Dersler",
                column: "OgretimGorevlisiId");

            migrationBuilder.CreateIndex(
                name: "IX_Notlar_DersId",
                table: "Notlar",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_Notlar_OgrenciId",
                table: "Notlar",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_Notlar_OgretimGorevlisiId",
                table: "Notlar",
                column: "OgretimGorevlisiId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_AppUserId",
                table: "Ogrenciler",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OgretimGorevlileri_AppUserId",
                table: "OgretimGorevlileri",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notlar");

            migrationBuilder.DropTable(
                name: "Dersler");

            migrationBuilder.DropTable(
                name: "Ogrenciler");

            migrationBuilder.DropTable(
                name: "OgretimGorevlileri");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
