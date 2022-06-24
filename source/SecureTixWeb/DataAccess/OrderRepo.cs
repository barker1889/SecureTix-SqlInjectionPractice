using Dapper;
using SecureTixWeb.DataAccess.Models;
using SecureTixWeb.DataAccess.Utils;

namespace SecureTixWeb.DataAccess
{
    public interface IOrderRepo
    {
        Task<int> CreateOrder(OrderDataModel order);
        Task CreateOrderItem(OrderItemDataModel orderItemDataModel);
        Task<OrderDataModel[]> GetOrdersForUser(int userId);
        Task<OrderItemDataModel[]> GetOrderItems(int orderId);
    }

    public class OrderRepo : IOrderRepo
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public OrderRepo(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateOrder(OrderDataModel order)
        {
            var sql = @"
INSERT INTO [dbo].[Orders]
           ([UserId]
           ,[ConfirmedAt]
           ,[TotalValue])
    OUTPUT INSERTED.OrderId
    VALUES
           (@userId
           ,@confirmedAt
           ,@totalValue)
";

            using (var con = _dbConnectionFactory.New())
            {
                return await con.QuerySingleAsync<int>(sql, new {userId = order.UserId, confirmedAt = order.ConfirmedAt, totalValue = order.TotalValue});
            }
        }

        public async Task CreateOrderItem(OrderItemDataModel orderItemDataModel)
        {
            var sql = @"
INSERT INTO [dbo].[OrderItems]
           ([OrderId]
           ,[EventId]
           ,[TicketCount]
           ,[PricePerTicket])
     VALUES
           (@orderId
           ,@eventId
           ,@ticketCount
           ,@pricePerTicket)
";
            using (var con = _dbConnectionFactory.New())
            {
                await con.ExecuteAsync(sql, new
                {
                    orderId = orderItemDataModel.OrderId, 
                    eventId = orderItemDataModel.EventId, 
                    ticketCount = orderItemDataModel.TicketCount,
                    pricePerTicket = orderItemDataModel.PricePerTicket
                });
            }
        }

        public async Task<OrderDataModel[]> GetOrdersForUser(int userId)
        {
            var sql = @"
SELECT [OrderId]
      ,[UserId]
      ,[ConfirmedAt]
      ,[RefundedAt]
      ,[TotalValue]
  FROM [dbo].[Orders]
  WHERE [UserId] = @userId
";
            using (var con = _dbConnectionFactory.New())
            {
                return (await con.QueryAsync<OrderDataModel>(sql, new {userId})).ToArray();
            }
        }

        public async Task<OrderItemDataModel[]> GetOrderItems(int orderId)
        {
            var sql = @"
SELECT [OrderId]
           ,[EventId]
           ,[TicketCount]
           ,[PricePerTicket]
FROM [OrderItems]
WHERE [OrderId] = @orderId
";
            using (var con = _dbConnectionFactory.New())
            {
                return (await con.QueryAsync<OrderItemDataModel>(sql, new { orderId })).ToArray();
            }
        }
    }
}
