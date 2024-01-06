using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Algorithmic_Trading.Migrations
{
    /// <inheritdoc />
    public partial class AddStockData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Ticker = table.Column<string>(type: "text", nullable: false),
                    Open = table.Column<float>(type: "real", nullable: false),
                    High = table.Column<float>(type: "real", nullable: false),
                    Low = table.Column<float>(type: "real", nullable: false),
                    Close = table.Column<float>(type: "real", nullable: false),
                    AdjustedClose = table.Column<float>(type: "real", nullable: false),
                    Volume = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => new { x.Ticker, x.Date });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
