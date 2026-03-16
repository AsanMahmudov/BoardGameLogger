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
        [ActionName("Create")]
        public async Task<IActionResult> Create(int id)
        {
            if (await _boardGameService.IsGameLoanedAsync(id))
            {
                // Redirect them back to details with a warning
                TempData["ErrorMessage"] = "This game is already loaned out!";
                return RedirectToAction("Details", "BoardGames", new { id = id });
            }

            var viewModel = await _loanGameService.GetLoanFormAsync(id);
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("Create")]
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
                // Add the error to ModelState so it shows up in the View
                ModelState.AddModelError("BorrowerName", ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id, int gameId)
        {
            await _loanGameService.ReturnGameAsync(id);

            // Redirect back to the game details to see the updated list
            return RedirectToAction("Details", "BoardGames", new { id = gameId });
        }


        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var loanedGames = await _loanGameService.GetAllLoansAsync();
            return View(loanedGames);
        }

    }
}
