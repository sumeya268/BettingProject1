namespace BettingAPI.Models
{
    public class Match
    {
        public int Id { get; set; }

        public int SportId { get; set; }
        public Sport? Sport { get; set; }

        public string TeamA { get; set; } = string.Empty;
        public string TeamB { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }

        public decimal OddsA { get; set; }
        public decimal OddsB { get; set; }
        public decimal OddsDraw { get; set; }

        public string Status { get; set; } = "Scheduled";

        public ICollection<BetLine> BetLines { get; set; } = new List<BetLine>();
    }
}

