using BoardGameLogger.Core.Interfaces;
using BoardGameLogger.Core.ViewModels;
using BoardGameLogger.Data;
using BoardGameLogger.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLogger.Core.Services
{
    public class LoanGameService : ILoanGameService
    {
        private readonly BoardGameLoggerDbContext _Dbcontext;
        public LoanGameService(BoardGameLoggerDbContext dbContext) => this._Dbcontext = dbContext;
        public async Task<LoanGameFormModel?> GetLoanFormAsync(int id)
        {
            var boardGame = await _Dbcontext.BoardGames.FindAsync(id);

            if (boardGame == null)
                throw new InvalidOperationException("Board game wasn't found.");

            var loanForm = new LoanGameFormModel
            {
                BoardGameId = boardGame.Id,
                BoardGameTitle = boardGame.Title
            };

            return loanForm;
        }

        public async Task AddLoanAsync(LoanGameFormModel model)
        {
            bool loanExists = await _Dbcontext.LoanLogs.Include(l => l.BoardGame)
                .AnyAsync(g => g.BorrowerName == model.BorrowerName && g.BoardGame.Title == model.BoardGameTitle);

            if (loanExists)
                throw new InvalidOperationException("Board game is already in library.");

            LoanLog newLoan = new LoanLog
            {
                BorrowerName = model.BorrowerName,
                BoardGameId = model.BoardGameId,
                LoanDate = model.LoanDate
            };

            _Dbcontext.LoanLogs.Add(newLoan);
            await _Dbcontext.SaveChangesAsync();
        }

        public async Task ReturnGameAsync(int loanId)
        {
            var loan = await _Dbcontext.LoanLogs.FindAsync(loanId);

            if (loan != null)
            {
                _Dbcontext.LoanLogs.Remove(loan);
                await _Dbcontext.SaveChangesAsync();
            }
        }
    }
}
