// This DTO is what we send to the frontend when returning matches.
// It keeps things simple so we don't expose the full Match model.
namespace BettingAPI.Dtos;

public class MatchDto
{
    public int Id { get; set; }
    public string Sport { get; set; } = null!;   // Sport name (Football, F1, etc.)
    public string Teams { get; set; } = null!;   // "Team A vs Team B"
    public DateTime StartTime { get; set; }      // When the match starts
    public decimal HomeOdds { get; set; }
    public decimal? DrawOdds { get; set; }
    public decimal? AwayOdds { get; set; }
}
