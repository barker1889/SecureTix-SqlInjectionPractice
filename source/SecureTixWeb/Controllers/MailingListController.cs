using Microsoft.AspNetCore.Mvc;
using SecureTixWeb.DataAccess;

namespace SecureTixWeb.Controllers
{
    public class MailingListController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IMailingListRepo _mailingListRepo;

        public MailingListController(IUserRepo userRepo, IMailingListRepo mailingListRepo)
        {
            _userRepo = userRepo;
            _mailingListRepo = mailingListRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(string email)
        {
            await _mailingListRepo.Insert(email);

            return RedirectToAction("Confirm");
        }

        [HttpGet]
        public IActionResult Confirm()
        {
            return View();
        }
    }
}
