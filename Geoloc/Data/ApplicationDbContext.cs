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

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<AppUserInMeeting> AppUserInMeetings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<AppUserInMeeting>()
                .HasKey(a => new {a.AppUserId, a.MeetingId});

            base.OnModelCreating(builder);
        }
    }
}
