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

            //Seeding Publishers
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Id = 1, Name = "Stonemaier Games", Country = "USA" },
                new Publisher { Id = 2, Name = "Czech Games Edition", Country = "Czech Republic" },
                new Publisher { Id = 3, Name = "Days of Wonder", Country = "France" },
                new Publisher { Id = 4, Name = "Leder Games", Country = "USA" },
                new Publisher { Id = 5, Name = "Lookout Games", Country = "Germany" },
                new Publisher { Id = 6, Name = "Cephalofair Games", Country = "USA" },
                new Publisher { Id = 7, Name = "Childreans", Country = "USA" },
                new Publisher { Id = 8, Name = "Plan B Games", Country = "Canada" },
                new Publisher { Id = 9, Name = "Z-Man Games", Country = "USA" },
                new Publisher { Id = 10, Name = "Horrible Guild", Country = "Italy" }
            );

            // Seeding our games
            modelBuilder.Entity<BoardGame>().HasData(
                new BoardGame { Id = 1, Title = "Wingspan", YearPublished = 2019, MinPlayers = 1, MaxPlayers = 5, Description = "Bird collection and engine building. Very relaxing.", PublisherId = 1 },
                new BoardGame { Id = 2, Title = "Codenames", YearPublished = 2015, MinPlayers = 2, MaxPlayers = 8, Description = "Social word game where you give one-word clues.", PublisherId = 2 },
                new BoardGame { Id = 3, Title = "Ticket to Ride", YearPublished = 2004, MinPlayers = 2, MaxPlayers = 5, Description = "Classic train route building game across North America.", PublisherId = 3 },
                new BoardGame { Id = 4, Title = "Root", YearPublished = 2018, MinPlayers = 2, MaxPlayers = 4, Description = "War in the forest where every player has totally different rules.", PublisherId = 4 },
                new BoardGame { Id = 5, Title = "Agricola", YearPublished = 2007, MinPlayers = 1, MaxPlayers = 5, Description = "Farming simulator where you try not to starve your family.", PublisherId = 5 },
                new BoardGame { Id = 6, Title = "Gloomhaven", YearPublished = 2017, MinPlayers = 1, MaxPlayers = 4, Description = "Massive dungeon crawl with a legacy campaign.", PublisherId = 6 },
                new BoardGame { Id = 7, Title = "Frosthaven", YearPublished = 2023, MinPlayers = 1, MaxPlayers = 4, Description = "The sequel to Gloomhaven with even more cardboard.", PublisherId = 7 },
                new BoardGame { Id = 8, Title = "Azul", YearPublished = 2017, MinPlayers = 2, MaxPlayers = 4, Description = "Beautiful tile-laying game about decorating a palace.", PublisherId = 8 },
                new BoardGame { Id = 9, Title = "Pandemic", YearPublished = 2008, MinPlayers = 2, MaxPlayers = 4, Description = "Cooperative game where you try to save the world from diseases.", PublisherId = 9 },
                new BoardGame { Id = 10, Title = "Railroad Ink", YearPublished = 2018, MinPlayers = 1, MaxPlayers = 6, Description = "A dice rolling game about drawing exits and routes.", PublisherId = 10 }
            );
        }

    }

}
