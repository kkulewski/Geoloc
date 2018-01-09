using System;
using System.Collections.Generic;

namespace Geoloc.Models.Entities
{
    public class Meeting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public int LocationId { get; set; }

        public List<AppUser> ParticipantUsers { get; set; }
        public Location Location { get; set; }
    }
}