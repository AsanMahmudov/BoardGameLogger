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

        public BoardGameService(BoardGameLoggerDbContext _context) => this._Dbcontext = _context;

        private BoardGameLoggerDbContext _Dbcontext;
        public async Task AddGameAsync(BoardGameFormModel model)
        {
            bool gameExists = await _Dbcontext.BoardGames
                .AnyAsync(g => g.Title == model.Title && g.YearPublished == model.YearPublished);

            if (gameExists)
            {
                throw new InvalidOperationException("Board game is already in library.");
            }


            BoardGame newGame = new BoardGame
            {
                Title = model.Title,
                YearPublished = model.YearPublished,
                MinPlayers = model.MinPlayers,
                MaxPlayers = model.MaxPlayers,
                Description = model.Description,
                PublisherId = model.SelectedPublisherId
            };


            await _Dbcontext.BoardGames.AddAsync(newGame);
            await _Dbcontext.SaveChangesAsync();
        }
        public async Task EditGameAsync(int id, BoardGameFormModel model)
        {
            var boardGame = await _Dbcontext.BoardGames.FindAsync(id);

            if (boardGame == null)
            {
                throw new InvalidOperationException("Board game not found.");
            }

            boardGame.Title = model.Title;
            boardGame.YearPublished = model.YearPublished;
            boardGame.MinPlayers = model.MinPlayers;
            boardGame.MaxPlayers = model.MaxPlayers;
            boardGame.Description = model.Description;
            boardGame.PublisherId = model.SelectedPublisherId;

            await _Dbcontext.SaveChangesAsync();
        }
        public async Task<IEnumerable<BoardGameIndexViewModel>> GetAllGamesAsync()
        {
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

                var publishers = await _Dbcontext.Publishers
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                    .ToListAsync();

            }
            return boardGame;
        }

        public async Task DeleteGameAsync(int id)
        {
            var boardGame = await _Dbcontext.BoardGames.FindAsync(id);

            if (boardGame == null)
                throw new InvalidOperationException("Board game not found.");   

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

            if (game == null) return null;

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
                    BorrowerName = l.BorrowerName,
                    LoanDate = l.LoanDate
                }).ToList()
            };


            return viewModelToReturn;
        }
    }
}
