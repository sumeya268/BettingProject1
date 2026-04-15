using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BettingAPI.Data;
using BettingAPI.Models;
using BettingAPI.Dtos;

namespace BettingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BetsController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public BetsController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        // POST /api/bets
        [HttpPost]
        public async Task<ActionResult> PlaceBet([FromBody] PlaceBetDto dto)
        {
            var user = await _ctx.Users.FindAsync(dto.UserId);
            if (user == null)
                return BadRequest("User not found.");

            if (dto.Stake <= 0 || dto.Selections.Count == 0)
                return BadRequest("Invalid bet.");

            if (user.Balance < dto.Stake)
                return BadRequest("Insufficient balance.");

            // Calculate total odds
            decimal totalOdds = 1m;
            foreach (var s in dto.Selections)
                totalOdds *= s.Odds;

            decimal potentialReturn = dto.Stake * totalOdds;

            // Create bet
            var bet = new Bet
            {
                UserId = user.Id,
                Stake = dto.Stake,
                PotentialReturn = potentialReturn,
                Date = DateTime.UtcNow
            };

            _ctx.Bets.Add(bet);
            await _ctx.SaveChangesAsync();

            // Add bet lines
            foreach (var s in dto.Selections)
            {
                var line = new BetLine
                {
                    BetId = bet.Id,
                    MatchId = s.MatchId,
                    SelectionType = s.SelectionType,
                    Odds = s.Odds
                };

                _ctx.BetLines.Add(line);
            }

            // Deduct stake
            user.Balance -= dto.Stake;
            await _ctx.SaveChangesAsync();

            return Ok(new
            {
                bet.Id,
                TotalOdds = totalOdds,
                bet.PotentialReturn,
                NewBalance = user.Balance
            });
        }

        // GET /api/bets/my?userId=1
        [HttpGet("my")]
        public async Task<ActionResult<IEnumerable<BetDto>>> GetMyBets([FromQuery] int userId)
        {
            var bets = await _ctx.Bets
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.Date)
                .Include(b => b.BetLines)
                    .ThenInclude(bl => bl.Match)
                .ToListAsync();

            var result = bets.Select(b => new BetDto
            {
                Id = b.Id,
                Stake = b.Stake,
                PotentialReturn = b.PotentialReturn,
                PlacedAt = b.Date,
                Lines = b.BetLines.Select(l => new BetLineDto
                {
                    MatchName = $"{l.Match.TeamA} vs {l.Match.TeamB}",
                    SelectionType = l.SelectionType,
                    Odds = l.Odds
                }).ToList()
            });

            return Ok(result);
        }
    }
}

