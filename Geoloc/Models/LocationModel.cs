using System;

namespace Geoloc.Models
{
    public class LocationModel
    {
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public UserModel User { get; set; }
        public long Timestamp { get; set; }
    }
}
