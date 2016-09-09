using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventManager.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Controllers
{
    public class VenuesController : Controller
    {
        private EventManagerContext db = new EventManagerContext();
        public IActionResult Index()
        {
            return View(db.Venues.ToList());
        }

        public IActionResult Details(int id)
        {
            var thisVenue = db.Venues
                .Include(venues => venues.Events)
                .FirstOrDefault(venues => venues.VenueId == id);
            return View(thisVenue);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Venue item)
        {
            db.Venues.Add(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisVenue = db.Venues.FirstOrDefault(items => items.VenueId == id);
            return View(thisVenue);
        }

        [HttpPost]
        public IActionResult Edit(Venue item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisVenue = db.Venues.FirstOrDefault(items => items.VenueId == id);
            return View(thisVenue);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisVenue = db.Venues.FirstOrDefault(items => items.VenueId == id);
            db.Venues.Remove(thisVenue);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}