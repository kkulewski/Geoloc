using System;
using System.Collections.Generic;

namespace Geoloc.Data.Entities
{
    public class Meeting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
    }
}
