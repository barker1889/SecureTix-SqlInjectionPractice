using SecureTixWeb.DataAccess;
using SecureTixWeb.DataAccess.Models;

namespace SecureTixWeb.Services
{
    public interface IBasketService
    {
        Task<(UserBasketDataModel, BasketItemDataModel[])> GetBasket(int basketId);
        Task AddItems(int basketId, int eventId, int ticketCount, double price);
        Task<UserBasketDataModel> GetOrCreateEmptyBasketForUser(int userUserId);
        Task<DateTime> SetBasketToPaid(int basketId);
    }

    public class BasketService : IBasketService
    {
        private readonly IBasketRepo _basketRepo;

        public BasketService(IBasketRepo basketRepo)
        {
            _basketRepo = basketRepo;
        }

        public async Task<(UserBasketDataModel, BasketItemDataModel[])> GetBasket(int basketId)
        {
            var basketItems = await _basketRepo.GetBasketItems(basketId);

            var basketModel = await _basketRepo.GetBasket(basketId);

            return (basketModel, basketItems);
        }

        public async Task AddItems(int basketId, int eventId, int ticketCount, double price)
        {
            await _basketRepo.AddBasketItem(new BasketItemDataModel
            {
                BasketId = basketId,
                EventId = eventId,
                TicketCount = ticketCount,
                PricePerTicket = price
            });
        }

        public async Task<UserBasketDataModel> GetOrCreateEmptyBasketForUser(int userId)
        {
            var existingBasket = await _basketRepo.GetUnpaidBasketForUser(userId);

            if (existingBasket != null)
            {
                return existingBasket;
            }

            return await _basketRepo.CreateNewBasketForUser(userId);
        }

        public async Task<DateTime> SetBasketToPaid(int basketId)
        {
            var confirmedAt = DateTime.UtcNow;
            await _basketRepo.SetBasketToPaid(basketId, confirmedAt);
            return confirmedAt;
        }
    }

    public class BasketItemViewModelRow
    {
        public int BasketId { get; set; }
        public string EventName { get; set; }
        public int TicketCount { get; set; }
        public double PricePerTicket { get; set; }
        public double TotalPrice => TicketCount * PricePerTicket;
    }
}
