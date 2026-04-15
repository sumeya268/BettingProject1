
// This DTO is what we send back when showing "My Bets".
// It contains the bet + all lines inside it.
namespace BettingAPI.Dtos
{
    public class BetDto
    {
        public int Id { get; set; }
        public decimal Stake { get; set; }
        public decimal PotentialReturn { get; set; }
        public DateTime PlacedAt { get; set; }
        public List<BetLineDto> Lines { get; set; } = new();
    }

    // Each line inside a bet when showing history
    public class BetLineDto
    {
        public string MatchName { get; set; } = null!;      // "Team A vs Team B"
        public string SelectionType { get; set; } = null!;  // Home / Draw / Away
        public decimal Odds { get; set; }
    }
}
