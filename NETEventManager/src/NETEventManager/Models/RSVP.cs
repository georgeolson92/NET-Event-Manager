using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Models
{
    [Table("RSVPs")]
    public class RSVP
    {
        [Key]
        public int RSVPId { get; set; }
        public virtual Event Event { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
