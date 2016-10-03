using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventManager.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using EventManager.ViewModels;

namespace EventManager.Controllers
{
    public class VenuesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public VenuesController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Venues.ToList());
        }

        public IActionResult Details(int id)
        {
            var thisVenue = _db.Venues
                .Include(venues => venues.Location)
                .FirstOrDefault(venues => venues.VenueId == id);
            var venueEvents = _db.Events.Where(items => items.VenueId == id).ToList();
            VenueViewModel venue = new VenueViewModel();
            venue.venueEvents = venueEvents;
            venue.thisVenue = thisVenue;
            return View(venue);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Venue item)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            item.Location = new Location();
            item.Location.address = Request.Form["address"];
            item.Location.city = Request.Form["city"];
            item.Location.state = Request.Form["state"];
            item.Location.country = Request.Form["country"];
            item.Location.postalCode = Request.Form["postalCode"];
            _db.Venues.Add(item);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisVenue = _db.Venues.FirstOrDefault(items => items.VenueId == id);
            return View(thisVenue);
        }

        [HttpPost]
        public IActionResult Edit(Venue item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisVenue = _db.Venues.FirstOrDefault(items => items.VenueId == id);
            return View(thisVenue);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisVenue = _db.Venues.FirstOrDefault(items => items.VenueId == id);
            _db.Venues.Remove(thisVenue);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DisplayVenuesByKeyword(string keyWord)
        {
            var searchVenueList = _db.Venues.Where(items => items.Name.Contains(keyWord));
            return Json(searchVenueList);
        }

        public IActionResult DisplayAllVenues()
        {
            return Json(_db.Venues.ToList());
        }

        public IActionResult PopulateVenues()
        {
            var venueList = Venue.GetVenues();
            foreach (var venue in venueList)
            {
                var thisVenue = _db.Venues.FirstOrDefault(venues => venues.Name == venue.Name);
                if (thisVenue == null)
                {
                    _db.Venues.Add(venue);
                    _db.SaveChanges();
                }
            }
            return Json(_db.Venues.ToList());
        }
    }
}