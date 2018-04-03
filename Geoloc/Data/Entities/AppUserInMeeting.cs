﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geoloc.Data.Entities
{
    public class AppUserInMeeting
    {
        [Key]
        public Guid AppUserId { get; set; }
        [Key]
        public Guid MeetingId { get; set; }
        public AppUser AppUser { get; set; }
        public Meeting Meeting { get; set; }
    }
}