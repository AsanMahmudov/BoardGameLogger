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
        }

    }

}
