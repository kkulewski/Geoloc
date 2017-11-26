using System.Collections.Generic;
using Geoloc.Models;

namespace Geoloc.Repository
{
    public class InMemoryLocationRepository : ILocationRepository
    {
        private static readonly IList<LocationModel> Locations = new List<LocationModel>();

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
