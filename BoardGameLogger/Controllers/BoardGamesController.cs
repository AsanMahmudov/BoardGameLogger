using BoardGameLogger.Core.Interfaces;
using BoardGameLogger.Core.ViewModels;
using BoardGameLogger.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoardGameLogger.Web.Controllers
{
    public class BoardGamesController : Controller
    {
        private readonly IBoardGameService _boardGameService;
        private readonly IPublisherService _publisherService;

        // Constructor injection - setting up our services so we can talk to the DB
        public BoardGamesController(IBoardGameService boardGameService, IPublisherService publisherService)
        {
            _boardGameService = boardGameService;
            _publisherService = publisherService;
        }

        public async Task<IActionResult> Index()
        {
            var boardGames = await _boardGameService.GetAllGamesAsync();
            return View(boardGames);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var publisherData = await _publisherService.GetAllPublishersAsync();

            // We need to populate the dropdown list for publishers here
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
        [ValidateAntiForgeryToken] // Security check to prevent CSRF attacks
        public async Task<IActionResult> Create(BoardGameFormModel model)
        {
            // If the user missed a field, we send them back to the form with the errors
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

            if (model == null) return NotFound();

            var publisherData = await _publisherService.GetAllPublishersAsync();

            model.Publishers = publisherData.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            });

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var game = await _boardGameService.GetGameByIdAsync(id);
            if (game == null) return NotFound();

            return View(new BoardGameIndexViewModel
            {
                Id = id,
                Title = game.Title,
                YearPublished = game.YearPublished
            });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
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

        public async Task<IActionResult> Details(int id)
        {
            var gameDetails = await _boardGameService.GetGameDetailsAsync(id);
            if (gameDetails == null) return NotFound();

            return View(gameDetails);
        }
    }
}