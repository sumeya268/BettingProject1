using System;

public class Class1
{
    public Class1()
    {
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using sports_betting.Models;

namespace sports_betting.Services
{
    public class DatabaseService
    {
        // Connects the program to your SQL database.
        private string connectionString = "Server=localhost;Database=BettingDB;Trusted_Connection=True;";

        // Loads all bets from the SQL database.
        public List<Bet> LoadBets()
        {
            List<Bet> bets = new List<Bet>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Bets";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bets.Add(new Bet
                        {
                            BetID = reader.GetInt32(0),
                            Sport = reader.GetString(1),
                            EventName = reader.GetString(2),
                            Selection = reader.GetString(3),
                            Odds = reader.GetDouble(4),
                            Stake = reader.GetDouble(5),
                            PotentialReturn = reader.GetDouble(6)
                        });
                    }
                }
            }

            return bets;
        }

        // Saves a single bet into the SQL database.
        public void SaveBet(Bet bet)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO Bets (BetID, Sport, EventName, Selection, Odds, Stake, PotentialReturn)
                                 VALUES (@BetID, @Sport, @EventName, @Selection, @Odds, @Stake, @Return)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BetID", bet.BetID);
                    cmd.Parameters.AddWithValue("@Sport", bet.Sport);
                    cmd.Parameters.AddWithValue("@EventName", bet.EventName);
                    cmd.Parameters.AddWithValue("@Selection", bet.Selection);
                    cmd.Parameters.AddWithValue("@Odds", bet.Odds);
                    cmd.Parameters.AddWithValue("@Stake", bet.Stake);
                    cmd.Parameters.AddWithValue("@Return", bet.PotentialReturn);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using sports_betting.Models;

namespace sports_betting.Services
{
    public class DatabaseService
    {
        // Connects the program to your SQL database.
        private string connectionString = "Server=localhost;Database=BettingDB;Trusted_Connection=True;";

        // Loads all bets from the SQL database.
        public List<Bet> LoadBets()
        {
            List<Bet> bets = new List<Bet>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Bets";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bets.Add(new Bet
                        {
                            BetID = reader.GetInt32(0),
                            Sport = reader.GetString(1),
                            EventName = reader.GetString(2),
                            Selection = reader.GetString(3),
                            Odds = reader.GetDouble(4),
                            Stake = reader.GetDouble(5),
                            PotentialReturn = reader.GetDouble(6)
                        });
                    }
                }
            }

            return bets;
        }

        // Saves a single bet into the SQL database.
        public void SaveBet(Bet bet)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO Bets (BetID, Sport, EventName, Selection, Odds, Stake, PotentialReturn)
                                 VALUES (@BetID, @Sport, @EventName, @Selection, @Odds, @Stake, @Return)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BetID", bet.BetID);
                    cmd.Parameters.AddWithValue("@Sport", bet.Sport);
                    cmd.Parameters.AddWithValue("@EventName", bet.EventName);
                    cmd.Parameters.AddWithValue("@Selection", bet.Selection);
                    cmd.Parameters.AddWithValue("@Odds", bet.Odds);
                    cmd.Parameters.AddWithValue("@Stake", bet.Stake);
                    cmd.Parameters.AddWithValue("@Return", bet.PotentialReturn);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}