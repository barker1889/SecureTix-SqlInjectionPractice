using Microsoft.AspNetCore.Mvc;
using SecureTixWeb.Setup;

namespace SecureTixWeb.Controllers
{
    public class ResetController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmReset()
        {
            await DatabaseReset.Run();

            return RedirectToAction("Index", "Home");
        }
    }
}
