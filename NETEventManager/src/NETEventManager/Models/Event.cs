using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventManager.Models
{
    [Table("Events")]
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int VenueId { get; set; }
        public virtual Venue Venue { get; set; }
        public virtual ApplicationUser User { get; set; }


        public override bool Equals(System.Object otherEvent)
        {
            if (!(otherEvent is Event))
            {
                return false;
            }
            else
            {
                Event newEvent = (Event)otherEvent;
                return this.EventId.Equals(newEvent.EventId);
            }
        }

        public override int GetHashCode()
        {
            return this.EventId.GetHashCode();
        }
    }

}
