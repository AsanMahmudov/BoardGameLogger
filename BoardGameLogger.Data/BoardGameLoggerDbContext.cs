using BoardGameLogger.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLogger.Data
{
    public class BoardGameLoggerDbContext : DbContext
    {
        public BoardGameLoggerDbContext(DbContextOptions<BoardGameLoggerDbContext> options)
        : base(options)
        {
        }

        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<LoanLog> LoanLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Seed Publishers
            // Note: Use negative IDs if you are using Identity columns to avoid conflicts
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Id = 1, Name = "Stonemaier Games", Country = "USA" },
                new Publisher { Id = 2, Name = "Asmodee", Country = "France" },
                new Publisher { Id = 3, Name = "Days of Wonder", Country = "USA" }
            );

            // 2. Seed Board Games
            modelBuilder.Entity<BoardGame>().HasData(
                new BoardGame
                {
                    Id = 1,
                    Title = "Wingspan",
                    YearPublished = 2019,
                    MinPlayers = 1,
                    MaxPlayers = 5,
                    Description = "A competitive, medium-weight, card-driven, engine-building board game.",
                    PublisherId = 1 // Matches Stonemaier
                },
                new BoardGame
                {
                    Id = 2,
                    Title = "Ticket to Ride",
                    YearPublished = 2004,
                    MinPlayers = 2,
                    MaxPlayers = 5,
                    Description = "A cross-country train adventure in which players collect and play matching train cards to claim railway routes.",
                    PublisherId = 3 // Matches Days of Wonder
                }
            );
        }

    }

}
