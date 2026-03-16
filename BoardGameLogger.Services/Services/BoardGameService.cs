using BoardGameLogger.Core.Interfaces;
using BoardGameLogger.Core.ViewModels;
using BoardGameLogger.Data;
using BoardGameLogger.Data.Models;
using BoardGameLogger.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLogger.Core.Services
{
    public class BoardGameService : IBoardGameService
    {
        // We inject DbContext through dependency injection 
        public BoardGameService(BoardGameLoggerDbContext _context) => this._Dbcontext = _context;

        private BoardGameLoggerDbContext _Dbcontext;
        public async Task AddGameAsync(BoardGameFormModel model)
        {
            //checking if game with same title and year already exists to prevent duplicates
            bool gameExists = await _Dbcontext.BoardGames
                .AnyAsync(g => g.Title == model.Title && g.YearPublished == model.YearPublished);

            if (gameExists)
            {
                throw new InvalidOperationException("Board game is already in library.");
            }

            //mapping to data model
            BoardGame newGame = new BoardGame
            {
                Title = model.Title,
                YearPublished = model.YearPublished,
                MinPlayers = model.MinPlayers,
                MaxPlayers = model.MaxPlayers,
                Description = model.Description,
                PublisherId = model.SelectedPublisherId
            };

            //saving to DB 
            await _Dbcontext.BoardGames.AddAsync(newGame);
            await _Dbcontext.SaveChangesAsync();
        }
        public async Task EditGameAsync(int id, BoardGameFormModel model)
        {
            //retrieving the game to edit from DB
            var boardGame = await _Dbcontext.BoardGames.FindAsync(id);

            if (boardGame == null)
            {
                throw new InvalidOperationException("Board game not found.");
            }

            //editing it's properties with the new values from the form
            boardGame.Title = model.Title;
            boardGame.YearPublished = model.YearPublished;
            boardGame.MinPlayers = model.MinPlayers;
            boardGame.MaxPlayers = model.MaxPlayers;
            boardGame.Description = model.Description;
            boardGame.PublisherId = model.SelectedPublisherId;

            //saving changes to the DB
            await _Dbcontext.SaveChangesAsync();
        }
        public async Task<IEnumerable<BoardGameIndexViewModel>> GetAllGamesAsync()
        {
            // retrieving all the games from the Db and map to our view model. Pretty Self explanatory
            List<BoardGameIndexViewModel> games = await _Dbcontext.BoardGames
                .Select(g => new BoardGameIndexViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    PublisherName = g.Publisher.Name,
                    YearPublished = g.YearPublished
                }).ToListAsync();

            return games;
        }
        public async Task<BoardGameFormModel?> GetGameByIdAsync(int id)
        {
            //retrieving game with ID and mapping to form model for edit form
            BoardGameFormModel? boardGame = await _Dbcontext.BoardGames
                .Where(g => g.Id == id)
                .Select(g => new BoardGameFormModel
                {

                    Title = g.Title,
                    YearPublished = g.YearPublished,
                    MinPlayers = g.MinPlayers,
                    MaxPlayers = g.MaxPlayers,
                    Description = g.Description,
                    SelectedPublisherId = g.PublisherId

                })
                .FirstOrDefaultAsync();

            if (boardGame != null)
            {
                //we populate Publishers property of the form model with all publishers from the DB to populate dropdown in edit form
                var publishers = await _Dbcontext.Publishers
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                    .ToListAsync();

            }

            return boardGame;
        }
        public async Task DeleteGameAsync(int id)
        {
            //retrieving game to delete from DB
            var boardGame = await _Dbcontext.BoardGames.FindAsync(id);

            if (boardGame == null)
                throw new InvalidOperationException("Board game not found.");   

            // saving changes. Duhhh
            _Dbcontext.BoardGames.Remove(boardGame);
            await _Dbcontext.SaveChangesAsync();

        }
        public async Task<BoardGameDetailsViewModel?> GetGameDetailsAsync(int id)
        {
            // Adding .Include for LoanLogs so we can see history
            var game = await _Dbcontext.BoardGames
                .Include(g => g.Publisher)
                .Include(g => g.LoanLogs)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null) 
                return null;

           /*we map the game to our details view model and also map the list of 
             LoanLogs to a list of BoardGameLoanInfoViewModel to show 
             loan history in details view and allow returning loaned games*/
            var viewModelToReturn = new BoardGameDetailsViewModel
            {
                Id = game.Id,
                Title = game.Title,
                YearPublished = game.YearPublished,
                MinPlayers = game.MinPlayers,
                MaxPlayers = game.MaxPlayers,
                Description = game.Description,
                PublisherName = game.Publisher.Name,
                LoanLogs = game.LoanLogs.Select(l => new BoardGameLoanInfoViewModel
                {
                    Id = l.Id,
                    BorrowerName = l.BorrowerName,
                    LoanDate = l.LoanDate
                }).ToList()
            };


            return viewModelToReturn;
        }
        public async Task<bool> IsGameLoanedAsync(int gameId)
        {
            //needed for LoansController to check if game is already loaned before allowing user to create a new loan for it
            return await _Dbcontext.LoanLogs.AnyAsync(l => l.BoardGameId == gameId);
        }
    }
}
