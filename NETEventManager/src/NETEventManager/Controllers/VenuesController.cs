using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventManager.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

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
                .Include(venues => venues.Events)
                .Include(venues => venues.User)
                .FirstOrDefault(venues => venues.VenueId == id);
            return View(thisVenue);
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
            item.User = currentUser;
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
    }
}