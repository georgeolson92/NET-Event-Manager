using Microsoft.AspNetCore.Mvc;
using EventManager.Models;
using System.Linq;

namespace EventManager.Controllers
{
    public class EventsController : Controller
    {
        private EventManagerContext db = new EventManagerContext();
        public IActionResult Index()
        {
            return View(db.Events.ToList());
        }

        public IActionResult Details(int id)
        {
            var thisEvent = db.Events.FirstOrDefault(items => items.EventId == id);
            return View(thisEvent);
        }
    }
}