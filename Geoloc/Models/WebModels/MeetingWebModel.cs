using System;
using System.Collections.Generic;

namespace Geoloc.Models.WebModels
{
    public class MeetingWebModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public UserWebModel Host { get; set; }
        public IEnumerable<UserWebModel> Participants { get; set; }
    }
}
