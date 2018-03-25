using System;

namespace Geoloc.Models
{
    public class LocationModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public UserModel User { get; set; }
        public long Timestamp { get; set; }
    }
}