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
                    Id = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    AssetId = table.Column<string>(nullable: true),
                    AssetQuoteId = table.Column<string>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetRates_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetRates_Assets_AssetQuoteId",
                        column: x => x.AssetQuoteId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetSymbols",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    AssetId = table.Column<string>(nullable: true),
                    AssetQuoteId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetSymbols", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetSymbols_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetSymbols_Assets_AssetQuoteId",
                        column: x => x.AssetQuoteId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetRates_AssetQuoteId",
                table: "AssetRates",
                column: "AssetQuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetRates_AssetId_AssetQuoteId",
                table: "AssetRates",
                columns: new[] { "AssetId", "AssetQuoteId" },
                unique: true,
                filter: "[AssetId] IS NOT NULL AND [AssetQuoteId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AssetSymbols_AssetQuoteId",
                table: "AssetSymbols",
                column: "AssetQuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetSymbols_AssetId_AssetQuoteId",
                table: "AssetSymbols",
                columns: new[] { "AssetId", "AssetQuoteId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetRates");

            migrationBuilder.DropTable(
                name: "AssetSymbols");

            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
