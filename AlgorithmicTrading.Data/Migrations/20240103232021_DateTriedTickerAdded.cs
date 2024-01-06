using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Algorithmic_Trading.Migrations
{
    /// <inheritdoc />
    public partial class DateTriedTickerAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DatesTried",
                table: "DatesTried");

            migrationBuilder.AddColumn<string>(
                name: "Ticker",
                table: "DatesTried",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DatesTried",
                table: "DatesTried",
                columns: new[] { "Ticker", "Date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DatesTried",
                table: "DatesTried");

            migrationBuilder.DropColumn(
                name: "Ticker",
                table: "DatesTried");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DatesTried",
                table: "DatesTried",
                column: "Date");
        }
    }
}
