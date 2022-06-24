using Microsoft.AspNetCore.Mvc;
using SecureTixWeb.Controllers.ViewModels;
using SecureTixWeb.DataAccess;

namespace SecureTixWeb.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventsRepo _eventsRepo;

        public EventsController(IEventsRepo eventsRepo)
        {
            _eventsRepo = eventsRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchTerm)
        {
            var model = new EventsListViewModel
            {
                SearchTerm = searchTerm,
                EventModels = (await _eventsRepo.List(searchTerm)).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromForm] string searchTerm)
        {
            return RedirectToAction("Index", "Events", new {searchTerm});
        }
    }
}
