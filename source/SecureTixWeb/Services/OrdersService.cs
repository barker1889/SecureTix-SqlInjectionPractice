using SecureTixWeb.DataAccess;
using SecureTixWeb.DataAccess.Models;

namespace SecureTixWeb.Services
{
    public interface IOrdersService
    {
        Task<int> CreateConfirmedOrder(int userId, List<OrderItemDataModel> orderItems, DateTime paidAt);
        Task<OrderDataModel[]> GetOrders(int userId);
        Task<OrderItemDataModel[]> GetOrderItems(int orderId);
    }

    public class OrdersService : IOrdersService
    {
        private readonly IOrderRepo _orderRepo;

        public OrdersService(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<int> CreateConfirmedOrder(int userId, List<OrderItemDataModel> orderItems, DateTime paidAt)
        {
            var orderDataModel = new OrderDataModel
            {
                UserId = userId,
                ConfirmedAt = paidAt,
                TotalValue = orderItems.Sum(i => i.TicketCount * i.PricePerTicket)
            };

            var orderId = await _orderRepo.CreateOrder(orderDataModel);

            foreach (var orderItem in orderItems)
            {
                orderItem.OrderId = orderId;
                await _orderRepo.CreateOrderItem(orderItem);
            }

            return orderId;
        }

        public async Task<OrderDataModel[]> GetOrders(int userId)
        {
            return await _orderRepo.GetOrdersForUser(userId);
        }

        public async Task<OrderItemDataModel[]> GetOrderItems(int orderId)
        {
            return await _orderRepo.GetOrderItems(orderId);
        }
    }
}
