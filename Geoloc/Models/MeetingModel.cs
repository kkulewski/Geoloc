using System;
using System.Collections.Generic;

namespace Geoloc.Models
{
    public class MeetingModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Description { get; set; }
        public UserModel Host { get; set; }
        public IEnumerable<UserModel> Participants { get; set; }
    }
}
