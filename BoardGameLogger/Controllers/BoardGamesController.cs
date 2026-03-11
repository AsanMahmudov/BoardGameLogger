using BoardGameLogger.Core.Interfaces;
using BoardGameLogger.Core.ViewModels;
using BoardGameLogger.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoardGameLogger.Web.Controllers
{
    public class BoardGamesController : Controller
    {

        private IBoardGameService _boardGameService;

        public BoardGamesController(IBoardGameService boardGameService)
        {
            _boardGameService = boardGameService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var boardGames = await _boardGameService.GetAllGamesAsync();

            return View(boardGames);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var publisherData = await _boardGameService.GetPublishers();

            var model = new BoardGameFormModel
            {

                Publishers = publisherData.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name            
                })
            };

            return View(model);
        }


    }
}
