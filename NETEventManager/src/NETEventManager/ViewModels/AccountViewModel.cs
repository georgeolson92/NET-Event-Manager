using EventManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.ViewModels
{
    public class AccountViewModel
    {
        public List<RSVP> eventRSVPs { get; set; }

        public List<Event> userEvents { get; set; }

        public ApplicationUser currentUser { get; set; }

    }
}