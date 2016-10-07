using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventManager.Models;
using EventManager.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Text.RegularExpressions;

namespace EventManager.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventsController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }


        public IActionResult Index()
        {
            return View(_db.Events.Include(events => events.Venue).ToList());
        }

        public async Task<IActionResult> Details(int id)
        {
            var thisEvent = _db.Events
               .Include(events => events.Venue)
               .Include(events => events.User)
               .FirstOrDefault(events => events.EventId == id);
            var eventRSVPs = _db.RSVPs.Include(rsvps => rsvps.User).Where(rsvps => rsvps.Event.EventId == id).ToList();
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var isRSVPed = false;
            if (currentUser == null)
            {
                isRSVPed = false;
            }
            else
            { 
                foreach (var rsvp in eventRSVPs)
                {
                    if (rsvp.User.Id == currentUser.Id)
                    {
                        isRSVPed = true;
                    }
                }
            }
            EventViewModel viewEvent = new EventViewModel();
            viewEvent.eventRSVPs = eventRSVPs;
            viewEvent.thisEvent = thisEvent;
            viewEvent.currentUser = currentUser;
            viewEvent.viewerIsRSVPed = isRSVPed;
            return View(viewEvent);
        }

        public IActionResult Create()
        {
            ViewBag.VenueId = new SelectList(_db.Venues, "VenueId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event item)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            item.User = currentUser;
            item.Time = Request.Form["eventTime"];
            _db.Events.Add(item);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisEvent = _db.Events.FirstOrDefault(items => items.EventId == id);
            ViewBag.VenueId = new SelectList(_db.Venues, "VenueId", "Name");
            return View(thisEvent);
        }

        [HttpPost]
        public IActionResult Edit(Event item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisEvent = _db.Events.FirstOrDefault(items => items.EventId == id);
            return View(thisEvent);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisEvent = _db.Events.FirstOrDefault(items => items.EventId == id);
            var eventRSVPs = _db.RSVPs.Include(rsvps => rsvps.User).Where(rsvps => rsvps.Event.EventId == id).ToList();
            foreach (var rsvp in eventRSVPs)
            {
              _db.RSVPs.Remove(rsvp);
            }
            _db.Events.Remove(thisEvent);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DisplayEventsByKeyword(string keyWord)
        {
            var searchEventList = _db.Events.Where(items => items.Title.Contains(keyWord));
            return Json(searchEventList);
        }

        public IActionResult DisplayAllEvents()
        {
            return Json(_db.Events.ToList());
        }


        public IActionResult AddToGoogleCalendar(int id)
        {
            var thisEvent = _db.Events.Include(events => events.Venue).FirstOrDefault(items => items.EventId == id);
            var calendarDate = thisEvent.Date.ToString("yyyyMMdd");
            var startTime = thisEvent.Time.ToString().Replace(":", "");
            var convertedTime = Int32.Parse(startTime);
            convertedTime = convertedTime + 600;
            startTime = convertedTime.ToString();
            var endConvertedTime = Int32.Parse(startTime) + 300;
            var endTime = endConvertedTime.ToString();
            var calendarUrl = "https://www.google.com/calendar/render?action=TEMPLATE&text=" + thisEvent.Title + "&dates=" + calendarDate + "T" + startTime + "00Z/20180320T" + endTime + "00Z&details=" + thisEvent.Description + "&location=" + thisEvent.Venue.Name + "&sf=true&output=xml";
            return Redirect(calendarUrl);
        }

        public async Task<IActionResult> Rsvp(int id)
        {
            RSVP thisRSVP = new RSVP();
            thisRSVP.Event = _db.Events.FirstOrDefault(events => events.EventId == id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            thisRSVP.User = await _userManager.FindByIdAsync(userId);
            _db.RSVPs.Add(thisRSVP);
            _db.SaveChanges();
            return Json("Successfully RSVPed!");
        }

        public async Task<IActionResult> CancelRsvp(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var thisRSVP = _db.RSVPs.FirstOrDefault(rsvps => rsvps.Event.EventId == id && rsvps.User.Id == userId);
            _db.RSVPs.Remove(thisRSVP);
            _db.SaveChanges();
            return Json("Cancelled RSVP.");
        }
    }
}