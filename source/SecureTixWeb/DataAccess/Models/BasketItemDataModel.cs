namespace SecureTixWeb.DataAccess.Models;

public class BasketItemDataModel
{
    public int BasketItemId { get; set; }
    public int BasketId { get; set; }
    public double PricePerTicket { get; set; }
    public int EventId { get; set; }
    public int TicketCount { get; set; }
}