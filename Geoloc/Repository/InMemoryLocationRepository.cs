using System.Collections.Generic;
using Geoloc.Models;

namespace Geoloc.Repository
{
    public class InMemoryLocationRepository : ILocationRepository
    {
        private static readonly IList<LocationModel> Locations = new List<LocationModel>
        {
            new LocationModel { Latitude = 53.101, Longitude = 18.505, Timestamp = 1511825100800, UserName = "john@test.com" },
            new LocationModel { Latitude = 53.122, Longitude = 18.515, Timestamp = 1511825105800, UserName = "john@test.com" },
            new LocationModel { Latitude = 51.005, Longitude = 17.805, Timestamp = 1511825105850, UserName = "anne@test.com" },
            new LocationModel { Latitude = 53.101, Longitude = 18.505, Timestamp = 1511825112900, UserName = "john@test.com" },
            new LocationModel { Latitude = 52.335, Longitude = 18.115, Timestamp = 1511825112950, UserName = "dave@test.com" },
            new LocationModel { Latitude = 53.125, Longitude = 18.525, Timestamp = 1511825121200, UserName = "john@test.com" }
        };

        public void Add(LocationModel model)
        {
            Locations.Add(model);
        }

        public IEnumerable<LocationModel> Get()
        {
            return Locations;
        }
    }
}
