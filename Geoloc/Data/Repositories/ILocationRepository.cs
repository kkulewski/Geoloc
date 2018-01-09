using System.Collections.Generic;
using Geoloc.Models;

namespace Geoloc.Repository
{
    public interface ILocationRepository
    {
        void Add(LocationWebModel webModel);

        IEnumerable<LocationWebModel> Get();
    }
}
