using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EventManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string AvatarURL { get; set; }
    }
}