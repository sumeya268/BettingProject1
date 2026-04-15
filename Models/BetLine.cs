namespace BettingAPI.Models
{
    public class BetLine
    {
        public int Id { get; set; }

        public int BetId { get; set; }
        public Bet Bet { get; set; }

        public int MatchId { get; set; }
        public Match Match { get; set; }

        public string SelectionType { get; set; }  // "Home", "Away", "Draw"

        public decimal Odds { get; set; }
    }
}
