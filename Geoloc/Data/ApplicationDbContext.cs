using System;
using Geoloc.Models.Entities;
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
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<UsersInMeeting> UsersInMeetings { get; set; }
        public DbSet<UserRelation> UserRelations { get; set; }
    }
}
