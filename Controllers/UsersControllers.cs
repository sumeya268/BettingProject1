using BettingAPI.Data;
using BettingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BettingAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/users/signup
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest req)
        {
            if (await _context.Users.AnyAsync(u => u.Username == req.Username))
                return BadRequest("Username already exists");

            if (await _context.Users.AnyAsync(u => u.Email == req.Email))
                return BadRequest("Email already exists");

            var user = new User
            {
                Username = req.Username,
                Email = req.Email,
                PasswordHash = req.Password, // store raw for now
                Balance = 100
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == req.Username && u.PasswordHash == req.Password);

            if (user == null)
                return BadRequest("Invalid username or password");

            return Ok(user);
        }
    }

    public class SignupRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

