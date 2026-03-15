using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGameLogger.Core.ViewModels;
using BoardGameLogger.Web.Models.ViewModels;

namespace BoardGameLogger.Core.Interfaces
{
    public interface IBoardGameService
    {
        Task<IEnumerable<BoardGameIndexViewModel>> GetAllGamesAsync();

        Task AddGameAsync(BoardGameFormModel model);

        Task<BoardGameFormModel?> GetGameByIdAsync(int id);

        Task EditGameAsync(int id, BoardGameFormModel model);

        Task DeleteGameAsync(int id);

        Task<BoardGameDetailsViewModel?> GetGameDetailsAsync(int id);
    }
}
