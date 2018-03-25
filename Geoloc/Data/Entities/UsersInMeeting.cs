using System;

namespace Geoloc.Data.Entities
{
    public class UsersInMeeting
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public Guid MeetingId { get; set; }
        public AppUser AppUser { get; set; }
        public Meeting Meeting { get; set; }
    }
}