using BoardGameLogger.Core.Interfaces;
using BoardGameLogger.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameLogger.Web.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        public async Task<IActionResult> Index()
        {
            var publishers = await _publisherService.GetAllPublishersAsync();
            return View(publishers);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PublisherFormModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                await _publisherService.AddPublisherAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException)
            {
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var publisher = await _publisherService.GetByIdAsync(id);
            if (publisher == null) return NotFound();

            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _publisherService.DeletePublisherAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}