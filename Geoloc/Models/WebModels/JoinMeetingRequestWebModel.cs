using System;

namespace Geoloc.Models.WebModels
{
    public class JoinMeetingRequestWebModel
    {
        public Guid UserId { get; set; }
        public Guid MeetingId { get; set; }
    }
}
