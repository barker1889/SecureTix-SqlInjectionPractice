using Microsoft.AspNetCore.Mvc;
using SecureTixWeb.Controllers.ViewModels;
using SecureTixWeb.DataAccess;
using SecureTixWeb.DataAccess.Models;
using SecureTixWeb.Services;
using SecureTixWeb.Utils;

namespace SecureTixWeb.Controllers
{
    public class BasketController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IBasketService _basketService;
        private readonly IEventsRepo _eventsRepo;
        private readonly IUserRepo _userRepo;
        private readonly IOrdersService _ordersService;

        public BasketController(
            ISessionService sessionService, 
            IBasketService basketService, 
            IEventsRepo eventsRepo,
            IUserRepo userRepo,
            IOrdersService ordersService)
        {
            _sessionService = sessionService;
            _basketService = basketService;
            _eventsRepo = eventsRepo;
            _userRepo = userRepo;
            _ordersService = ordersService;
        }

        [HttpGet]
        [Route("basket/{basketId?}")]
        public async Task<IActionResult> Index(int? basketId)
        {
            if (!Request.TryGetSessionId(out var sessionId))
            {
                return RedirectToAction("Index", "Login", new {returnUrl = "/basket"});
            }

            if (!basketId.HasValue)
            {
                var user = _sessionService.ResolveUser(sessionId.Value);
                var userBasket = await _basketService.GetOrCreateEmptyBasketForUser(user.UserId);
                return RedirectToAction("Index", new {basketId = userBasket.BasketId});
            }

            var (basket, basketItems) = await _basketService.GetBasket(basketId.Value);
            var basketOwner = await _userRepo.GetById(basket.UserId);
            var itemsViewData = await CreateBasketItemsViewData(basketItems);

            var basketItemsViewModel = new BasketItemsViewModel
            {
                BasketId = basket.BasketId,
                PaidAt = basket.PaidAt,
                BasketItems = itemsViewData,
                Owner = basketOwner.Username
            };

            if (basket.PaidAt.HasValue)
            {
                return View("BasketAlreadyPaid", basketItemsViewModel);
            }

            return View(basketItemsViewModel);
        }

        private async Task<IEnumerable<BasketItemViewModelRow>> CreateBasketItemsViewData(BasketItemDataModel[] basketItems)
        {
            var results = new List<BasketItemViewModelRow>();

            foreach (var basketItemDataModel in basketItems)
            {
                var @event = await _eventsRepo.Get(basketItemDataModel.EventId);
                results.Add(new BasketItemViewModelRow()
                {
                    BasketId = basketItemDataModel.BasketId,
                    EventName = @event.Name,
                    PricePerTicket = basketItemDataModel.PricePerTicket,
                    TicketCount = basketItemDataModel.TicketCount
                });
            }

            return results;
        }

        [HttpPost]
        public async Task<IActionResult> Add(int eventId, int ticketCount)
        {
            if (!Request.TryGetSessionId(out var sessionId))
            {
                return RedirectToAction("Index", "Login", new { returnUrl = "/events" });
            }

            var user = _sessionService.ResolveUser(sessionId.Value);

            var eventToAdd = await _eventsRepo.Get(eventId);

            if (!eventToAdd.OnSale)
            {
                return View("EventNotFound");
            }

            if (eventToAdd.SoldOut)
            {
                return View("SoldOut");
            }

            var basket = await _basketService.GetOrCreateEmptyBasketForUser(user.UserId);

            await _basketService.AddItems(basket.BasketId, eventToAdd.Id, ticketCount, eventToAdd.Price);

            return RedirectToAction("Index", "Events");
        }

        [HttpPost]
        public async Task<IActionResult> Pay()
        {
            if (!Request.TryGetSessionId(out var sessionId))
            {
                return RedirectToAction("Index", "Login", new { returnUrl = "/events" });
            }

            var user = _sessionService.ResolveUser(sessionId.Value);

            var currentBasket = await _basketService.GetOrCreateEmptyBasketForUser(user.UserId);
            
            var (basket, basketItems) = await _basketService.GetBasket(currentBasket.BasketId);

            foreach (var basketItem in basketItems)
            {
                if ((await _eventsRepo.Get(basketItem.EventId)).SoldOut)
                {
                    return View("SoldOut");
                }
            }

            var orderItems = basketItems.Select(i => new OrderItemDataModel
            {
                PricePerTicket = i.PricePerTicket,
                EventId = i.EventId,
                TicketCount = i.TicketCount
            });

            if (!orderItems.Any())
            {
                ViewBag.Username = user.Username;
                return View("BasketEmpty");
            }

            var confirmedDateTime = await _basketService.SetBasketToPaid(currentBasket.BasketId);
            var orderId = await _ordersService.CreateConfirmedOrder(basket.UserId, orderItems.ToList(), confirmedDateTime);

            return RedirectToAction("Details", "Orders", new { orderId });
        }
    }
}
