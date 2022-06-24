namespace SecureTixWeb.DataAccess.Models;

public class UserBasketDataModel
{
    public int UserId { get; set; }
    public int BasketId { get; set; }
    public DateTime? PaidAt { get; set; }
}