﻿using Microsoft.EntityFrameworkCore;


namespace EventManager.Models
{
    public class EventManagerContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        public DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=NETEventManager;integrated security=True");
        }
    }
}