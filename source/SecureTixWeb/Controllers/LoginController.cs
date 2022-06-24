using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SecureTixWeb.DataAccess;
using SecureTixWeb.Services;

namespace SecureTixWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly ISessionService _sessionService;

        public LoginController(IUserRepo userRepo, ISessionService sessionService)
        {
            _userRepo = userRepo;
            _sessionService = sessionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string errorMessage, string returnUrl)
        {
            ViewBag.ErrorMessage = errorMessage;
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn([FromForm] string username, [FromForm] string password, [FromForm] string returnUrl)
        {
            var user = await _userRepo.TryResolveUser(username, ToMd5(password));

            if (user == null)
            {
                return RedirectToAction("Index",
                    new
                    {
                        errorMessage = $"login failed for user '{username}', either username or password is incorrect"
                    });
            }

            var userSession = _sessionService.CreateNewUserSession(user);
            
            Response.Cookies.Append("SessionId", userSession.SessionId.ToString());

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            if (Request.Cookies.ContainsKey("SessionId"))
            {
                Response.Cookies.Delete("SessionId");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            await _userRepo.CreateUser(userModel, ToMd5(userModel.Password));

            return RedirectToAction("Index", "Login");
        }

        private string ToMd5(string input)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hash = md5.ComputeHash(inputBytes);
                return BitConverter
                    .ToString(hash)
                    .Replace("-", string.Empty)
                    .ToLower();
            }

        }
    }
}
