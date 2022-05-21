using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MagniseCryptocurrenciesApp.DataAccess.Migrations
{
    public partial class InitDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PriceUSD = table.Column<decimal>(nullable: true),
                    TypeIsCrypto = table.Column<bool>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetRates",
                columns: table => new
                {
                    AssetId = table.Column<string>(nullable: false),
                    AssetIdQuote = table.Column<string>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetRates", x => new { x.AssetId, x.AssetIdQuote });
                    table.ForeignKey(
                        name: "FK_AssetRates_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetRates");

            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
