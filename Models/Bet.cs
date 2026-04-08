namespace BettingAPI.Models
{
    public class Bet
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public decimal Stake { get; set; }
        public decimal PotentialReturn { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public List<BetLine> BetLines { get; set; } = new();
    }
}
