namespace SecureTixWeb.DataAccess.Models;

public class EventDataModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public double Price { get; set; }
    public bool OnSale { get; set; }
    public bool SoldOut { get; set; }
}