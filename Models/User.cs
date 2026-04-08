namespace BettingAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public decimal Balance { get; set; }

        public List<Bet> Bets { get; set; } = new();
    }
}
