﻿using System;
using System.Collections.Generic;

namespace Geoloc.Data.Entities
{
    public class Meeting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public Guid LocationId { get; set; }

        public List<AppUser> ParticipantUsers { get; set; }
        public Location Location { get; set; }
    }
}