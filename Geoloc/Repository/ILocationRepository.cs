using System.Collections.Generic;
using Geoloc.Models;

namespace Geoloc.Repository
{
    public interface ILocationRepository
    {
        void Add(LocationModel model);

        IEnumerable<LocationModel> Get();
    }
}
