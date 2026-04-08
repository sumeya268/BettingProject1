using Microsoft.EntityFrameworkCore;
using BettingAPI.Models;

namespace BettingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<BetLine> BetLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User → Bets (1-to-many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Bets)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Bet → BetLines (1-to-many)
            modelBuilder.Entity<Bet>()
                .HasMany(b => b.BetLines)
                .WithOne(bl => bl.Bet)
                .HasForeignKey(bl => bl.BetId)
                .OnDelete(DeleteBehavior.Cascade);

            // Match → BetLines (1-to-many)
            modelBuilder.Entity<Match>()
                .HasMany(m => m.BetLines)
                .WithOne(bl => bl.Match)
                .HasForeignKey(bl => bl.MatchId)
                .OnDelete(DeleteBehavior.Restrict);

            DataSeeder.Seed(modelBuilder);
        }
    }
    }

