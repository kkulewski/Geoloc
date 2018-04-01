using System;
using System.Collections.Generic;
using Geoloc.Data.Entities;

namespace Geoloc.Data.Repositories.Abstract
{
    public interface ILocationRepository
    {
        Location Get(Guid id);
        IEnumerable<Location> GetLocationsByUser(Guid userId);
        IEnumerable<Location> GetAllLocations();
        void Add(Location model);
    }
}
