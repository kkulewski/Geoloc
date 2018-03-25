using System;

namespace Geoloc.Models.WebModels
{
    public class MeetingWebModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid LocationId { get; set; }
        public Guid UserId { get; set; }
    }
}
