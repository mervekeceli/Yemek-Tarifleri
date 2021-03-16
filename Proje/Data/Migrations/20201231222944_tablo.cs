using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proje.Data.Migrations
{
    public partial class tablo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategori",
                columns: table => new
                {
                    KategoriId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAdi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategori", x => x.KategoriId);
                });

            migrationBuilder.CreateTable(
                name: "Malzeme",
                columns: table => new
                {
                    MalzemeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MalzemeAdi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Malzeme", x => x.MalzemeId);
                });

            migrationBuilder.CreateTable(
                name: "Yemek",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(nullable: true),
                    Tarif = table.Column<string>(nullable: true),
                    Resim = table.Column<string>(nullable: true),
                    KacKisilik = table.Column<int>(nullable: true),
                    HazirlikSuresi = table.Column<int>(nullable: true),
                    PisirmeSuresi = table.Column<int>(nullable: true),
                    YuklemeTarihi = table.Column<DateTime>(nullable: false),
                    KategoriId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yemek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Yemek_Kategori_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategori",
                        principalColumn: "KategoriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YemekMalzeme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YemekId = table.Column<int>(nullable: false),
                    MalzemeId = table.Column<int>(nullable: false),
                    MalzemeMiktari = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YemekMalzeme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YemekMalzeme_Malzeme_MalzemeId",
                        column: x => x.MalzemeId,
                        principalTable: "Malzeme",
                        principalColumn: "MalzemeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YemekMalzeme_Yemek_YemekId",
                        column: x => x.YemekId,
                        principalTable: "Yemek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Yemek_KategoriId",
                table: "Yemek",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_YemekMalzeme_MalzemeId",
                table: "YemekMalzeme",
                column: "MalzemeId");

            migrationBuilder.CreateIndex(
                name: "IX_YemekMalzeme_YemekId",
                table: "YemekMalzeme",
                column: "YemekId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YemekMalzeme");

            migrationBuilder.DropTable(
                name: "Malzeme");

            migrationBuilder.DropTable(
                name: "Yemek");

            migrationBuilder.DropTable(
                name: "Kategori");
        }
    }
}
