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
        private IPublisherService _publisherService;

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
            var publisherData = await _publisherService.GetAllPublishersAsync();

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

        [HttpPost]
        public async Task<IActionResult> Create(BoardGameFormModel model)
        {
            if (!ModelState.IsValid)
            {
                var publisherData = await _publisherService.GetAllPublishersAsync();
                model.Publishers = publisherData.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                });

                return View(model);
            }

            await _boardGameService.AddGameAsync(model);
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _boardGameService.GetGameByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, BoardGameFormModel model)
        {
            if (!ModelState.IsValid)
            {
                var publishers = await _publisherService.GetAllPublishersAsync();
                model.Publishers = publishers.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                });


                return View(model);
            }

            await _boardGameService.EditGameAsync(id, model);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _boardGameService.DeleteGameAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

    }
}
