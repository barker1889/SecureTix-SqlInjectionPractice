using SecureTixWeb.Services;

namespace SecureTixWeb.Controllers.ViewModels;

public class BasketItemsViewModel
{
    public int BasketId { get; set; }
    public DateTime? PaidAt { get; set; }
    public IEnumerable<BasketItemViewModelRow> BasketItems { get; set; }
    public string Owner { get; set; }
}