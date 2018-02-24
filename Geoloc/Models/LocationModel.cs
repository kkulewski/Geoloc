using System;

namespace Geoloc.Models
{
    public class LocationModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Username { get; set; }
        public Guid UserId { get; set; }
        public long Timestamp { get; set; }
    }
}