﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventManager.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

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

        public IActionResult Details(int id)
        {
            var thisEvent = _db.Events
               .Include(events => events.Venue)
               .Include(events => events.User)
               .FirstOrDefault(experiences => experiences.EventId == id);
            return View(thisEvent);
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
            _db.Events.Remove(thisEvent);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}