using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BoardGameLogger.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BoardGames",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Bird collection and engine building. Very relaxing.");

            migrationBuilder.UpdateData(
                table: "BoardGames",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "MaxPlayers", "PublisherId", "Title", "YearPublished" },
                values: new object[] { "Social word game where you give one-word clues.", 8, 2, "Codenames", 2015 });

            migrationBuilder.InsertData(
                table: "BoardGames",
                columns: new[] { "Id", "Description", "MaxPlayers", "MinPlayers", "PublisherId", "Title", "YearPublished" },
                values: new object[] { 3, "Classic train route building game across North America.", 5, 2, 3, "Ticket to Ride", 2004 });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Country", "Name" },
                values: new object[] { "Czech Republic", "Czech Games Edition" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Country",
                value: "France");

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[,]
                {
                    { 4, "USA", "Leder Games" },
                    { 5, "Germany", "Lookout Games" },
                    { 6, "USA", "Cephalofair Games" },
                    { 7, "USA", "Childreans" },
                    { 8, "Canada", "Plan B Games" },
                    { 9, "USA", "Z-Man Games" },
                    { 10, "Italy", "Horrible Guild" }
                });

            migrationBuilder.InsertData(
                table: "BoardGames",
                columns: new[] { "Id", "Description", "MaxPlayers", "MinPlayers", "PublisherId", "Title", "YearPublished" },
                values: new object[,]
                {
                    { 4, "War in the forest where every player has totally different rules.", 4, 2, 4, "Root", 2018 },
                    { 5, "Farming simulator where you try not to starve your family.", 5, 1, 5, "Agricola", 2007 },
                    { 6, "Massive dungeon crawl with a legacy campaign.", 4, 1, 6, "Gloomhaven", 2017 },
                    { 7, "The sequel to Gloomhaven with even more cardboard.", 4, 1, 7, "Frosthaven", 2023 },
                    { 8, "Beautiful tile-laying game about decorating a palace.", 4, 2, 8, "Azul", 2017 },
                    { 9, "Cooperative game where you try to save the world from diseases.", 4, 2, 9, "Pandemic", 2008 },
                    { 10, "A dice rolling game about drawing exits and routes.", 6, 1, 10, "Railroad Ink", 2018 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BoardGames",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BoardGames",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BoardGames",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BoardGames",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BoardGames",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "BoardGames",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "BoardGames",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "BoardGames",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "BoardGames",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "A competitive, medium-weight, card-driven, engine-building board game.");

            migrationBuilder.UpdateData(
                table: "BoardGames",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "MaxPlayers", "PublisherId", "Title", "YearPublished" },
                values: new object[] { "A cross-country train adventure in which players collect and play matching train cards to claim railway routes.", 5, 3, "Ticket to Ride", 2004 });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Country", "Name" },
                values: new object[] { "France", "Asmodee" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Country",
                value: "USA");
        }
    }
}
