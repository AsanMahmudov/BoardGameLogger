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

        Task EditGameAsync(int id, BoardGameFormModel model);

        // We'll need this for the Create view to populate the Publisher dropdown
        Task<IEnumerable<PublisherSelectionViewModel>> GetPublishers();
    }
}
