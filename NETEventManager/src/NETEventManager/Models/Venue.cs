using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager.Models
{
    [Table("Venues")]
    public class Venue
    {
        public Venue()
        {
            this.Events = new HashSet<Event>();
        }

        [Key]
        public int VenueId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}