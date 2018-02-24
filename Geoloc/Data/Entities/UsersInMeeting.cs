using System;

namespace Geoloc.Data.Entities
{
    public class UsersInMeeting
    {
        public int Id { get; set; }
        
        public Guid AppUserId { get; set; }
        public int MeetingId { get; set; }
        public AppUser AppUser { get; set; }
        public Meeting Meeting { get; set; }
    }
}