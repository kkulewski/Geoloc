using System;
using Geoloc.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Geoloc.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, UserRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<UsersInEvent> UsersInMeetings { get; set; }
        public DbSet<UserRelation> UserRelations { get; set; }
    }
}
