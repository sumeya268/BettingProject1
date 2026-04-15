namespace BettingAPI.Models
{
    public class Sport
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public List<Match> Matches { get; set; } = new();
    }
}

