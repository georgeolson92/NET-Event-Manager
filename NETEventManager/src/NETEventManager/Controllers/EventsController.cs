using Microsoft.AspNetCore.Mvc;
using EventManager.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Event item)
        {
            db.Events.Add(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisEvent = db.Events.FirstOrDefault(items => items.EventId == id);
            return View(thisEvent);
        }

        [HttpPost]
        public IActionResult Edit(Event item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisEvent = db.Events.FirstOrDefault(items => items.EventId == id);
            return View(thisEvent);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisEvent = db.Events.FirstOrDefault(items => items.EventId == id);
            db.Events.Remove(thisEvent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}