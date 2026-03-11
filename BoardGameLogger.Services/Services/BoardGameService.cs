using BoardGameLogger.Core.Interfaces;
using BoardGameLogger.Core.ViewModels;
using BoardGameLogger.Data;
using BoardGameLogger.Data.Models;
using BoardGameLogger.Web.Models.ViewModels;
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

        public BoardGameService(BoardGameLoggerDbContext _context)
        {
            this.Dbcontext = _context;
        }

        private BoardGameLoggerDbContext Dbcontext;
        public async Task AddGameAsync(BoardGameFormModel model)
        {
            bool exists = await Dbcontext.BoardGames
                .AnyAsync(g => g.Title == model.Title && g.YearPublished == model.YearPublished);

            if (exists)
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



            await Dbcontext.BoardGames.AddAsync(newGame);
            await Dbcontext.SaveChangesAsync();
        }


        public async Task EditGameAsync(int id, BoardGameFormModel model)
        {
            var boardGame = await Dbcontext.BoardGames.FindAsync(id);

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

            await Dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BoardGameIndexViewModel>> GetAllGamesAsync()
        {
            List<BoardGameIndexViewModel> games = await Dbcontext.BoardGames
                .Select(g => new BoardGameIndexViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    PublisherName = g.Publisher.Name,
                    YearPublished = g.YearPublished
                }).ToListAsync();

            return games;
        }

        public async Task<IEnumerable<PublisherSelectionViewModel>> GetPublishers()
        {
            List<PublisherSelectionViewModel> publishers = await Dbcontext.Publishers
                .Select(p => new PublisherSelectionViewModel
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToListAsync();

            return publishers;
        }
    }
}
