using System;
using System.Collections.Generic;
using Geoloc.Data.Entities;

namespace Geoloc.Models
{
    public class MeetingModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public LocationModel Location { get; set; }
        public UserModel User { get; set; }
    }
}
