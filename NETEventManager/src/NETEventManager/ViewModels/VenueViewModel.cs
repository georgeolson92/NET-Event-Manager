using EventManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.ViewModels
{
    public class VenueViewModel
    {
        public List<Event> venueEvents { get; set; }

        public Venue thisVenue { get; set; }
    }
}