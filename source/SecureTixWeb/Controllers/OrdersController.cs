using Microsoft.AspNetCore.Mvc;
using SecureTixWeb.DataAccess;
using SecureTixWeb.DataAccess.Models;
using SecureTixWeb.Services;
using SecureTixWeb.Utils;

namespace SecureTixWeb.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly ISessionService _sessionService;
        private readonly IEventsRepo _eventsRepo;

        public OrdersController(
            IOrdersService ordersService, 
            ISessionService sessionService,
            IEventsRepo eventsRepo)
        {
            _ordersService = ordersService;
            _sessionService = sessionService;
            _eventsRepo = eventsRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!Request.TryGetSessionId(out var sessionId))
            {
                return RedirectToAction("Index", "Login", new { returnUrl = "/events" });
            }

            var user = _sessionService.ResolveUser(sessionId.Value);

            var orders = await _ordersService.GetOrders(user.UserId);

            return View(new OrdersListViewModel
            {
                Orders = orders
            });
        }

        [HttpGet]
        [Route("orders/{orderId}/details")]
        public async Task<IActionResult> Details(int orderId)
        {
            if (!Request.TryGetSessionId(out var sessionId))
            {
                return RedirectToAction("Index", "Login", new { returnUrl = "/events" });
            }

            var user = _sessionService.ResolveUser(sessionId.Value);
            var order = (await _ordersService.GetOrders(user.UserId))
                .Single(o => o.OrderId == orderId);

            var orderItems = await _ordersService.GetOrderItems(order.OrderId);
            
            var orderItemsViewData = new List<OrderItemViewModel>();
            foreach (var orderItemDataModel in orderItems)
            {
                var @event = await _eventsRepo.Get(orderItemDataModel.EventId);
                orderItemsViewData.Add(new OrderItemViewModel
                {
                    EventName = @event.Name,
                    EventDescription = @event.Description,
                    PricePerTicket = orderItemDataModel.PricePerTicket,
                    TicketCount = orderItemDataModel.TicketCount
                });
            }

            return View(new OrderDetailViewModel
            {
                Order = order,
                Items = orderItemsViewData.ToArray()
            });
        }
    }

    public class OrdersListViewModel
    {
        public OrderDataModel[] Orders { get; set; }
    }

    public class OrderDetailViewModel
    {
        public OrderDataModel Order { get; set; }
        public OrderItemViewModel[] Items { get; set; }
    }

    public class OrderItemViewModel
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public int TicketCount { get; set; }
        public double PricePerTicket { get; set; }

    }
}
