using Microsoft.AspNetCore.Mvc;
using SecureTixWeb.Controllers.ViewComponents.ViewModels;
using SecureTixWeb.Services;
using SecureTixWeb.Utils;

namespace SecureTixWeb.Controllers.ViewComponents
{
    public class UserLoginComponent : ViewComponent
    {
        private readonly ISessionService _sessionService;

        public UserLoginComponent(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Request.TryGetSessionId(out var sessionId);

            var model = new LoginComponentViewModel();

            if (!sessionId.HasValue)
            {
                return View("LoginComponent", model);
            }

            var user = _sessionService.ResolveUser(sessionId.Value);
            if (user != null)
            {
                model.LoggedIn = true;
                model.UserName = user.Username;
                model.UserRole = user.Role;
            }
            
            return View("LoginComponent", model);
        }
    }
}
