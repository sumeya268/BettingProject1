using BettingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BettingAPI.Data
{
    public static class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Seed Sports
            modelBuilder.Entity<Sport>().HasData(
                new Sport { Id = 1, Name = "Football" },
                new Sport { Id = 2, Name = "Basketball" },
                new Sport { Id = 3, Name = "American Football" },
                new Sport { Id = 4, Name = "Horse Racing" },
                new Sport { Id = 5, Name = "Formula 1" }
            );

            // Seed Matches (STATIC VALUES — no dynamic DateTime, no random odds)
            modelBuilder.Entity<Match>().HasData(
                new Match { Id = 1, SportId = 1, TeamA = "Arsenal", TeamB = "Chelsea", StartTime = new DateTime(2026, 4, 6, 1, 1, 9, DateTimeKind.Utc), OddsA = 3.70m, OddsB = 2.17m, OddsDraw = 1.62m, Status = "Scheduled" },
                new Match { Id = 2, SportId = 1, TeamA = "Liverpool", TeamB = "Manchester United", StartTime = new DateTime(2026, 4, 7, 7, 1, 9, DateTimeKind.Utc), OddsA = 2.75m, OddsB = 4.87m, OddsDraw = 1.28m, Status = "Scheduled" },
                new Match { Id = 3, SportId = 1, TeamA = "Manchester City", TeamB = "Tottenham", StartTime = new DateTime(2026, 4, 7, 17, 1, 9, DateTimeKind.Utc), OddsA = 2.89m, OddsB = 1.72m, OddsDraw = 1.51m, Status = "Scheduled" },
                new Match { Id = 4, SportId = 1, TeamA = "Newcastle", TeamB = "Aston Villa", StartTime = new DateTime(2026, 4, 5, 13, 1, 9, DateTimeKind.Utc), OddsA = 4.02m, OddsB = 4.38m, OddsDraw = 4.06m, Status = "Scheduled" },
                new Match { Id = 5, SportId = 1, TeamA = "West Ham", TeamB = "Brighton", StartTime = new DateTime(2026, 4, 8, 9, 1, 9, DateTimeKind.Utc), OddsA = 4.69m, OddsB = 4.76m, OddsDraw = 3.18m, Status = "Scheduled" },
                new Match { Id = 6, SportId = 1, TeamA = "Everton", TeamB = "Fulham", StartTime = new DateTime(2026, 4, 6, 12, 1, 9, DateTimeKind.Utc), OddsA = 1.22m, OddsB = 2.05m, OddsDraw = 2.97m, Status = "Scheduled" },
                new Match { Id = 7, SportId = 1, TeamA = "Leeds", TeamB = "Leicester", StartTime = new DateTime(2026, 4, 5, 16, 1, 9, DateTimeKind.Utc), OddsA = 3.71m, OddsB = 3.30m, OddsDraw = 3.05m, Status = "Scheduled" },
                new Match { Id = 8, SportId = 1, TeamA = "Southampton", TeamB = "Crystal Palace", StartTime = new DateTime(2026, 4, 7, 18, 1, 9, DateTimeKind.Utc), OddsA = 1.26m, OddsB = 1.92m, OddsDraw = 4.62m, Status = "Scheduled" },

                // Basketball
                new Match { Id = 9, SportId = 2, TeamA = "Lakers", TeamB = "Warriors", StartTime = new DateTime(2026, 4, 8, 23, 1, 9, DateTimeKind.Utc), OddsA = 1.50m, OddsB = 4.85m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 10, SportId = 2, TeamA = "Celtics", TeamB = "Heat", StartTime = new DateTime(2026, 4, 9, 0, 1, 9, DateTimeKind.Utc), OddsA = 3.83m, OddsB = 2.61m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 11, SportId = 2, TeamA = "Bulls", TeamB = "Knicks", StartTime = new DateTime(2026, 4, 5, 22, 1, 9, DateTimeKind.Utc), OddsA = 1.21m, OddsB = 4.44m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 12, SportId = 2, TeamA = "Nets", TeamB = "76ers", StartTime = new DateTime(2026, 4, 9, 21, 1, 9, DateTimeKind.Utc), OddsA = 2.44m, OddsB = 2.40m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 13, SportId = 2, TeamA = "Suns", TeamB = "Mavericks", StartTime = new DateTime(2026, 4, 7, 5, 1, 9, DateTimeKind.Utc), OddsA = 3.51m, OddsB = 1.65m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 14, SportId = 2, TeamA = "Clippers", TeamB = "Nuggets", StartTime = new DateTime(2026, 4, 6, 12, 1, 9, DateTimeKind.Utc), OddsA = 2.30m, OddsB = 4.48m, OddsDraw = 0m, Status = "Scheduled" },

                // American Football
                new Match { Id = 15, SportId = 3, TeamA = "Patriots", TeamB = "Jets", StartTime = new DateTime(2026, 4, 6, 15, 1, 9, DateTimeKind.Utc), OddsA = 3.96m, OddsB = 3.60m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 16, SportId = 3, TeamA = "Cowboys", TeamB = "Eagles", StartTime = new DateTime(2026, 4, 6, 22, 1, 9, DateTimeKind.Utc), OddsA = 2.11m, OddsB = 1.33m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 17, SportId = 3, TeamA = "Packers", TeamB = "Bears", StartTime = new DateTime(2026, 4, 6, 2, 1, 9, DateTimeKind.Utc), OddsA = 2.49m, OddsB = 5.00m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 18, SportId = 3, TeamA = "Chiefs", TeamB = "Broncos", StartTime = new DateTime(2026, 4, 6, 23, 1, 9, DateTimeKind.Utc), OddsA = 4.84m, OddsB = 1.66m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 19, SportId = 3, TeamA = "Rams", TeamB = "49ers", StartTime = new DateTime(2026, 4, 8, 2, 1, 9, DateTimeKind.Utc), OddsA = 4.61m, OddsB = 4.69m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 20, SportId = 3, TeamA = "Bills", TeamB = "Dolphins", StartTime = new DateTime(2026, 4, 8, 12, 1, 9, DateTimeKind.Utc), OddsA = 2.58m, OddsB = 4.69m, OddsDraw = 0m, Status = "Scheduled" },

                // Horse Racing
                new Match { Id = 21, SportId = 4, TeamA = "Thunderbolt", TeamB = "Night Fury", StartTime = new DateTime(2026, 4, 8, 8, 1, 9, DateTimeKind.Utc), OddsA = 4.52m, OddsB = 3.94m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 22, SportId = 4, TeamA = "Silver Arrow", TeamB = "Wild Spirit", StartTime = new DateTime(2026, 4, 7, 4, 1, 9, DateTimeKind.Utc), OddsA = 4.06m, OddsB = 2.21m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 23, SportId = 4, TeamA = "Golden Mane", TeamB = "Storm Runner", StartTime = new DateTime(2026, 4, 5, 21, 1, 9, DateTimeKind.Utc), OddsA = 2.05m, OddsB = 4.01m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 24, SportId = 4, TeamA = "Rapid Hooves", TeamB = "Shadow Dancer", StartTime = new DateTime(2026, 4, 4, 23, 1, 9, DateTimeKind.Utc), OddsA = 4.06m, OddsB = 3.03m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 25, SportId = 4, TeamA = "Blaze Runner", TeamB = "Iron Stallion", StartTime = new DateTime(2026, 4, 5, 1, 1, 9, DateTimeKind.Utc), OddsA = 2.19m, OddsB = 1.40m, OddsDraw = 0m, Status = "Scheduled" },

                // Formula 1
                new Match { Id = 26, SportId = 5, TeamA = "Max Verstappen", TeamB = "Lewis Hamilton", StartTime = new DateTime(2026, 4, 6, 12, 1, 9, DateTimeKind.Utc), OddsA = 3.98m, OddsB = 3.24m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 27, SportId = 5, TeamA = "Charles Leclerc", TeamB = "Lando Norris", StartTime = new DateTime(2026, 4, 6, 18, 1, 9, DateTimeKind.Utc), OddsA = 2.47m, OddsB = 2.47m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 28, SportId = 5, TeamA = "Fernando Alonso", TeamB = "George Russell", StartTime = new DateTime(2026, 4, 7, 8, 1, 9, DateTimeKind.Utc), OddsA = 1.66m, OddsB = 4.37m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 29, SportId = 5, TeamA = "Carlos Sainz", TeamB = "Sergio Perez", StartTime = new DateTime(2026, 4, 7, 10, 1, 9, DateTimeKind.Utc), OddsA = 2.15m, OddsB = 1.52m, OddsDraw = 0m, Status = "Scheduled" },
                new Match { Id = 30, SportId = 5, TeamA = "Oscar Piastri", TeamB = "Valtteri Bottas", StartTime = new DateTime(2026, 4, 5, 17, 1, 9, DateTimeKind.Utc), OddsA = 2.92m, OddsB = 1.88m, OddsDraw = 0m, Status = "Scheduled" }
            );
        }
    }
}
