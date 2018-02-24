using System;

namespace Geoloc.Data.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime CreatedOn { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}