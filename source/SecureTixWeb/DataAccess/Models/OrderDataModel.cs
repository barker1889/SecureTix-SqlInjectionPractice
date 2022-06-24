namespace SecureTixWeb.DataAccess.Models;

public class OrderDataModel
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public DateTime ConfirmedAt { get; set; }
    public DateTime? RefundedAt { get; set; }
    public double TotalValue { get; set; }
}