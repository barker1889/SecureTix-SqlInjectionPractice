using Microsoft.AspNetCore.Mvc;
using SecureTixWeb.Controllers.ViewComponents.ViewModels;
using SecureTixWeb.Services;
using SecureTixWeb.Utils;

namespace SecureTixWeb.Controllers.ViewComponents
{
    public class BasketComponent : ViewComponent
    {
        private readonly ISessionService _sessionService;
        private readonly IBasketService _basketService;

        public BasketComponent(ISessionService sessionService, IBasketService basketService)
        {
            _sessionService = sessionService;
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Request.TryGetSessionId(out var sessionId);

            if (!sessionId.HasValue)
            {
                return View("BasketComponent", new BasketComponentViewModel()
                {
                    NumberOfItems = 0
                });
            }

            var user = _sessionService.ResolveUser(sessionId.Value);

            if (user == null)
            {
                return View("BasketComponent", new BasketComponentViewModel()
                {
                    NumberOfItems = 0
                });
            }

            var basket = await _basketService.GetOrCreateEmptyBasketForUser(user.UserId);
            var (userBasket, items) = await _basketService.GetBasket(basket.BasketId);

            return View("BasketComponent", new BasketComponentViewModel()
            {
                NumberOfItems = items.Length
            });
        }
    }
}
