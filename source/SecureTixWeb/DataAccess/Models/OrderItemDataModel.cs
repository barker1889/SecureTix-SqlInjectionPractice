namespace SecureTixWeb.DataAccess.Models;

public class OrderItemDataModel
{
    public int OrderId { get; set; }
    public int EventId { get; set; }
    public int TicketCount { get; set; }
    public double PricePerTicket { get; set; }
}