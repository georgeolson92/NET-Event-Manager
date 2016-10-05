using EventManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.ViewModels
{
    public class EventViewModel
    {
        public List<RSVP> eventRSVPs { get; set; }

        public Event thisEvent { get; set; }
    }
}