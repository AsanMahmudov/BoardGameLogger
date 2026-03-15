using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BoardGameLogger.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "YearPublished",
                table: "BoardGames",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[,]
                {
                    { 1, "USA", "Stonemaier Games" },
                    { 2, "France", "Asmodee" },
                    { 3, "USA", "Days of Wonder" }
                });

            migrationBuilder.InsertData(
                table: "BoardGames",
                columns: new[] { "Id", "Description", "MaxPlayers", "MinPlayers", "PublisherId", "Title", "YearPublished" },
                values: new object[,]
                {
                    { 1, "A competitive, medium-weight, card-driven, engine-building board game.", 5, 1, 1, "Wingspan", 2019 },
                    { 2, "A cross-country train adventure in which players collect and play matching train cards to claim railway routes.", 5, 2, 3, "Ticket to Ride", 2004 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BoardGames",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BoardGames",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "YearPublished",
                table: "BoardGames",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
