// This DTO is what the frontend sends when placing a bet.
// It contains the user, stake, and all selections.
namespace BettingAPI.Dtos;

public class PlaceBetDto
{
    public int UserId { get; set; }              // Who is placing the bet
    public decimal Stake { get; set; }           // How much they are betting
    public List<PlaceBetLineDto> Selections { get; set; } = new();
}

// Each selection inside the bet slip
public class PlaceBetLineDto
{
    public int MatchId { get; set; }             // Which match they selected
    public string SelectionType { get; set; } = null!; // Home / Draw / Away
    public decimal Odds { get; set; }            // Odds at the time of placing
}
