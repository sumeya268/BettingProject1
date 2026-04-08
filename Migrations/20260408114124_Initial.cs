using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BettingAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SportId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamA = table.Column<string>(type: "TEXT", nullable: false),
                    TeamB = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OddsA = table.Column<decimal>(type: "TEXT", nullable: false),
                    OddsB = table.Column<decimal>(type: "TEXT", nullable: false),
                    OddsDraw = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Stake = table.Column<decimal>(type: "TEXT", nullable: false),
                    PotentialReturn = table.Column<decimal>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BetLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BetId = table.Column<int>(type: "INTEGER", nullable: false),
                    MatchId = table.Column<int>(type: "INTEGER", nullable: false),
                    SelectionType = table.Column<string>(type: "TEXT", nullable: false),
                    Odds = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BetLines_Bets_BetId",
                        column: x => x.BetId,
                        principalTable: "Bets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BetLines_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Sports",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Football" },
                    { 2, "Basketball" },
                    { 3, "American Football" },
                    { 4, "Horse Racing" },
                    { 5, "Formula 1" }
                });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "Id", "OddsA", "OddsB", "OddsDraw", "SportId", "StartTime", "Status", "TeamA", "TeamB" },
                values: new object[,]
                {
                    { 1, 3.70m, 2.17m, 1.62m, 1, new DateTime(2026, 4, 6, 1, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Arsenal", "Chelsea" },
                    { 2, 2.75m, 4.87m, 1.28m, 1, new DateTime(2026, 4, 7, 7, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Liverpool", "Manchester United" },
                    { 3, 2.89m, 1.72m, 1.51m, 1, new DateTime(2026, 4, 7, 17, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Manchester City", "Tottenham" },
                    { 4, 4.02m, 4.38m, 4.06m, 1, new DateTime(2026, 4, 5, 13, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Newcastle", "Aston Villa" },
                    { 5, 4.69m, 4.76m, 3.18m, 1, new DateTime(2026, 4, 8, 9, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "West Ham", "Brighton" },
                    { 6, 1.22m, 2.05m, 2.97m, 1, new DateTime(2026, 4, 6, 12, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Everton", "Fulham" },
                    { 7, 3.71m, 3.30m, 3.05m, 1, new DateTime(2026, 4, 5, 16, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Leeds", "Leicester" },
                    { 8, 1.26m, 1.92m, 4.62m, 1, new DateTime(2026, 4, 7, 18, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Southampton", "Crystal Palace" },
                    { 9, 1.50m, 4.85m, 0m, 2, new DateTime(2026, 4, 8, 23, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Lakers", "Warriors" },
                    { 10, 3.83m, 2.61m, 0m, 2, new DateTime(2026, 4, 9, 0, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Celtics", "Heat" },
                    { 11, 1.21m, 4.44m, 0m, 2, new DateTime(2026, 4, 5, 22, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Bulls", "Knicks" },
                    { 12, 2.44m, 2.40m, 0m, 2, new DateTime(2026, 4, 9, 21, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Nets", "76ers" },
                    { 13, 3.51m, 1.65m, 0m, 2, new DateTime(2026, 4, 7, 5, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Suns", "Mavericks" },
                    { 14, 2.30m, 4.48m, 0m, 2, new DateTime(2026, 4, 6, 12, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Clippers", "Nuggets" },
                    { 15, 3.96m, 3.60m, 0m, 3, new DateTime(2026, 4, 6, 15, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Patriots", "Jets" },
                    { 16, 2.11m, 1.33m, 0m, 3, new DateTime(2026, 4, 6, 22, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Cowboys", "Eagles" },
                    { 17, 2.49m, 5.00m, 0m, 3, new DateTime(2026, 4, 6, 2, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Packers", "Bears" },
                    { 18, 4.84m, 1.66m, 0m, 3, new DateTime(2026, 4, 6, 23, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Chiefs", "Broncos" },
                    { 19, 4.61m, 4.69m, 0m, 3, new DateTime(2026, 4, 8, 2, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Rams", "49ers" },
                    { 20, 2.58m, 4.69m, 0m, 3, new DateTime(2026, 4, 8, 12, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Bills", "Dolphins" },
                    { 21, 4.52m, 3.94m, 0m, 4, new DateTime(2026, 4, 8, 8, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Thunderbolt", "Night Fury" },
                    { 22, 4.06m, 2.21m, 0m, 4, new DateTime(2026, 4, 7, 4, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Silver Arrow", "Wild Spirit" },
                    { 23, 2.05m, 4.01m, 0m, 4, new DateTime(2026, 4, 5, 21, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Golden Mane", "Storm Runner" },
                    { 24, 4.06m, 3.03m, 0m, 4, new DateTime(2026, 4, 4, 23, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Rapid Hooves", "Shadow Dancer" },
                    { 25, 2.19m, 1.40m, 0m, 4, new DateTime(2026, 4, 5, 1, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Blaze Runner", "Iron Stallion" },
                    { 26, 3.98m, 3.24m, 0m, 5, new DateTime(2026, 4, 6, 12, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Max Verstappen", "Lewis Hamilton" },
                    { 27, 2.47m, 2.47m, 0m, 5, new DateTime(2026, 4, 6, 18, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Charles Leclerc", "Lando Norris" },
                    { 28, 1.66m, 4.37m, 0m, 5, new DateTime(2026, 4, 7, 8, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Fernando Alonso", "George Russell" },
                    { 29, 2.15m, 1.52m, 0m, 5, new DateTime(2026, 4, 7, 10, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Carlos Sainz", "Sergio Perez" },
                    { 30, 2.92m, 1.88m, 0m, 5, new DateTime(2026, 4, 5, 17, 1, 9, 0, DateTimeKind.Utc), "Scheduled", "Oscar Piastri", "Valtteri Bottas" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BetLines_BetId",
                table: "BetLines",
                column: "BetId");

            migrationBuilder.CreateIndex(
                name: "IX_BetLines_MatchId",
                table: "BetLines",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_UserId",
                table: "Bets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_SportId",
                table: "Matches",
                column: "SportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BetLines");

            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Sports");
        }
    }
}
