using BoardGameLogger.Core.Interfaces;
using BoardGameLogger.Core.ViewModels;
using BoardGameLogger.Data;
using BoardGameLogger.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardGameLogger.Core.Services
{
    public class LoanGameService : ILoanGameService
    {
        private readonly BoardGameLoggerDbContext _Dbcontext;

        // We inject DbContext through dependency injection - standard stuff
        public LoanGameService(BoardGameLoggerDbContext dbContext) => this._Dbcontext = dbContext;

        public async Task<LoanGameFormModel?> GetLoanFormAsync(int id)
        {
            // Finding the game first so the loan form knows what game we are even talking about
            var boardGame = await _Dbcontext.BoardGames.FindAsync(id);

            if (boardGame == null)
                throw new InvalidOperationException("Board game wasn't found.");

            // Mapping the game info to the form model
            var loanForm = new LoanGameFormModel
            {
                BoardGameId = boardGame.Id,
                BoardGameTitle = boardGame.Title,
                LoanDate = DateTime.Now // Defaulting to now so the user doesn't have to type it
            };

            return loanForm;
        }

        public async Task AddLoanAsync(LoanGameFormModel model)
        {
            // Basic check to see if this person already has this exact game out
            // Good for preventing weird data duplicates
            bool loanExists = await _Dbcontext.LoanLogs.Include(l => l.BoardGame)
                .AnyAsync(g => g.BorrowerName == model.BorrowerName && g.BoardGame.Title == model.BoardGameTitle);

            if (loanExists)
                throw new InvalidOperationException("This person already has a loan for this game!");

            // Creating the new log entry
            LoanLog newLoan = new LoanLog
            {
                BorrowerName = model.BorrowerName,
                BoardGameId = model.BoardGameId,
                LoanDate = model.LoanDate
            };

            // Saving to the database
            _Dbcontext.LoanLogs.Add(newLoan);
            await _Dbcontext.SaveChangesAsync();
        }

        public async Task ReturnGameAsync(int loanId)
        {
            // We find the specific loan record to delete/return the loaned out game
            var loan = await _Dbcontext.LoanLogs.FindAsync(loanId);

            if (loan != null)
            {
                // Removing the log returns the game in our system to be loaned out again
                _Dbcontext.LoanLogs.Remove(loan);
                await _Dbcontext.SaveChangesAsync();
            }
        }
    }
}