using System.Collections.Generic;
using Geoloc.Models.Entities;

namespace Geoloc.Data.Repositories
{
    public interface ILocationRepository
    {
        void Add(Location model);
        IEnumerable<Location> GetByUser(string userId);
        IEnumerable<Location> GetAllLocations();
    }
}
