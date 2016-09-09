using Microsoft.EntityFrameworkCore;

namespace EventManager.Models
{
    public class EventManagerContext : DbContext
    {
        public virtual DbSet<Event> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=NETEventManager;integrated security=True");
        }
    }
}