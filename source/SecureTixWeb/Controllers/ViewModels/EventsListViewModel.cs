using SecureTixWeb.DataAccess.Models;

namespace SecureTixWeb.Controllers.ViewModels;

public class EventsListViewModel
{
    public string SearchTerm { get; set; }
    public List<EventDataModel> EventModels { get; set; }
}