using System;
using System.ComponentModel.DataAnnotations;

namespace Geoloc.Data.Entities
{
    public class UserInMeeting
    {
        [Key]
        public Guid UserId { get; set; }
        [Key]
        public Guid MeetingId { get; set; }
        public User User { get; set; }
        public Meeting Meeting { get; set; }
    }
}
