using BoardGameLogger.Core.Interfaces;
using BoardGameLogger.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameLogger.Web.Controllers
{
    public class LoansController : Controller
    {
        private readonly ILoanGameService _loanGameService;
        private readonly IBoardGameService _boardGameService;

        public LoansController(ILoanGameService loanGameService, IBoardGameService boardGameService)
        {
            _loanGameService = loanGameService;
            _boardGameService = boardGameService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Shows a list of all lending history across the app
            var loanedGames = await _loanGameService.GetAllLoansAsync();
            return View(loanedGames);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            // Business rule: Can't loan out a game that's already gone!
            if (await _boardGameService.IsGameLoanedAsync(id))
            {
                TempData["ErrorMessage"] = "This game is already loaned out!";
                return RedirectToAction("Details", "BoardGames", new { id = id });
            }

            var viewModel = await _loanGameService.GetLoanFormAsync(id);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanGameFormModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                await _loanGameService.AddLoanAsync(model);
                return RedirectToAction("Details", "BoardGames", new { id = model.BoardGameId });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("BorrowerName", ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id, int gameId)
        {
            // Processing the return and sending them back to the game page
            await _loanGameService.ReturnGameAsync(id);
            return RedirectToAction("Details", "BoardGames", new { id = gameId });
        }
    }
}