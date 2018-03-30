using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geoloc.Data.Entities
{
    public class Meeting
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Description { get; set; }
        public Guid HostId { get; set; }

        [ForeignKey("HostId")]
        public AppUser Host { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
