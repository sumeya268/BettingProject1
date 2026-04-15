using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BettingAPI.Data;
using BettingAPI.Dtos;
using BettingAPI.Models;

namespace BettingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public MatchesController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        // ---------------------------------------------------------
        // GET /api/matches  (Load all matches for betting page)
        // ---------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetMatches()
        {
            var matches = await _ctx.Matches
                .Include(m => m.Sport)
                .ToListAsync();

            var result = matches.Select(m => new MatchDto
            {
                Id = m.Id,
                Sport = m.Sport.Name,
                Teams = $"{m.TeamA} vs {m.TeamB}",
                StartTime = m.StartTime,
                HomeOdds = m.OddsA,
                DrawOdds = m.OddsDraw,
                AwayOdds = m.OddsB
            });

            return Ok(result);
        }

        // ---------------------------------------------------------
        // POST /api/matches  (Create a new match)
        // ---------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> CreateMatch([FromBody] Match match)
        {
            _ctx.Matches.Add(match);
            await _ctx.SaveChangesAsync();
            return Ok(match);
        }

        // ---------------------------------------------------------
        // GET /api/matches/results  (Load finished matches only)
        // ---------------------------------------------------------
        [HttpGet("results")]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetResults()
        {
            var finishedMatches = await _ctx.Matches
                .Include(m => m.Sport)
                .Where(m => m.Status == "Finished")
                .ToListAsync();

            var result = finishedMatches.Select(m => new MatchDto
            {
                Id = m.Id,
                Sport = m.Sport.Name,
                Teams = $"{m.TeamA} vs {m.TeamB}",
                StartTime = m.StartTime,
                HomeOdds = m.OddsA,
                DrawOdds = m.OddsDraw,
                AwayOdds = m.OddsB
            });

            return Ok(result);
        }
    }
}

