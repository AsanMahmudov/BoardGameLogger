using BoardGameLogger.Core.Interfaces;
using BoardGameLogger.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameLogger.Web.Controllers
{
    public class LoansController : Controller
    {
        private readonly IBoardGameService _gameService;
        public LoansController(IBoardGameService gameService) => _gameService = gameService;

        [HttpGet]
        [ActionName("Create")]
        public async Task<IActionResult> CreateGet(int id)
        {

            var viewModel = await _gameService.GetLoanFormAsync(id);

            if (viewModel == null)
                return NotFound();

            return View(viewModel); 
        }
        [HttpPost]
        [ActionName("Create")] 
        public async Task<IActionResult> CreatePost(LoanGameFormModel model)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = await _gameService.GetLoanFormAsync(model.BoardGameId);

                if (viewModel == null)
                    return NotFound();

                return View(viewModel);
            }

            await _gameService.AddLoanAsync(model);

            //Redirecting to Details so they can see the loan they just added
            return RedirectToAction("Details", "BoardGames", new { id = model.BoardGameId });
        }
    }
}
