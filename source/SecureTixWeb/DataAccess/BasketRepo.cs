using Dapper;
using SecureTixWeb.DataAccess.Models;
using SecureTixWeb.DataAccess.Utils;

namespace SecureTixWeb.DataAccess;

public interface IBasketRepo
{
    Task<UserBasketDataModel> GetUnpaidBasketForUser(int userId);
    Task AddBasketItem(BasketItemDataModel basketItemDataModel);
    Task<BasketItemDataModel[]> GetBasketItems(int basketId);
    Task SetBasketToPaid(int basketId, DateTime confirmedAt);
    Task<UserBasketDataModel> CreateNewBasketForUser(int userId);
    Task<UserBasketDataModel> GetBasket(int basketId);
}

public class BasketRepo : IBasketRepo
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public BasketRepo(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<UserBasketDataModel> GetUnpaidBasketForUser(int userId)
    {
        var sql = @"
SELECT [BasketId], [UserId], [PaidAt]
    FROM [UserBaskets]
    WHERE [PaidAt] IS NULL AND [UserId] = @userId
";
        using (var con = _dbConnectionFactory.New())
        {
            return await con.QueryFirstOrDefaultAsync<UserBasketDataModel>(sql, new {userId});
        }
    }

    public async Task<UserBasketDataModel> GetBasket(int basketId)
    {
        var sql = @"
SELECT [BasketId], [UserId], [PaidAt]
    FROM [UserBaskets]
    WHERE [BasketId] = @basketId
";
        using (var con = _dbConnectionFactory.New())
        {
            return await con.QueryFirstOrDefaultAsync<UserBasketDataModel>(sql, new {basketId});
        }
    }

    public async Task<BasketItemDataModel[]> GetBasketItems(int basketId)
    {
        var sql = @"
SELECT * FROM [UserBasketItems] WHERE [BasketId] = @basketId
";

        using (var con = _dbConnectionFactory.New())
        {
            return (await con.QueryAsync<BasketItemDataModel>(sql, new { basketId })).ToArray();
        }
    }

    public async Task SetBasketToPaid(int basketId, DateTime paidAt)
    {
        var sql = @"
UPDATE [UserBaskets]
SET [PaidAt] = @paidAt
WHERE [BasketId] = @basketId
";

        using (var con = _dbConnectionFactory.New())
        {
            await con.ExecuteAsync(sql, new { basketId, paidAt = paidAt });
        }
    }

    public async Task AddBasketItem(BasketItemDataModel basketItemDataModel)
    {
        var sql = @"
INSERT INTO [UserBasketItems] ([BasketId], [EventId], [PricePerTicket], [TicketCount])
VALUES (@basketId, @eventId, @pricePerTicket, @ticketCount)
";

        using (var con = _dbConnectionFactory.New())
        {
            await con.ExecuteAsync(sql, new
            {
                basketId = basketItemDataModel.BasketId,
                eventId = basketItemDataModel.EventId,
                pricePerTicket = basketItemDataModel.PricePerTicket,
                ticketCount = basketItemDataModel.TicketCount
            });
        }
    }

    public async Task<UserBasketDataModel> CreateNewBasketForUser(int userId)
    {
        var sql = @"
INSERT INTO [UserBaskets] ([UserId])
OUTPUT INSERTED.BasketId
VALUES (@UserId)
";
        using (var con = _dbConnectionFactory.New())
        {
            var basketId = await con.QuerySingleAsync<int>(sql, new {userId});
            return new UserBasketDataModel
            {
                BasketId = basketId,
                UserId = userId
            };
        }
    }
}