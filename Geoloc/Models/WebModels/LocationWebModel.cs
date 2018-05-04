using System;

namespace Geoloc.Models.WebModels
{
    public class LocationWebModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Username { get; set; }
        public Guid UserId { get; set; }
        public long Timestamp { get; set; }
    }
}
