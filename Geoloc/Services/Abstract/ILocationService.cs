using System;
using System.Collections.Generic;
using Geoloc.Models;

namespace Geoloc.Services.Abstract
{
    public interface ILocationService
    {
        void AddLocation(LocationModel model);
        IEnumerable<LocationModel> GetLocationByUserId(Guid userId);
        IEnumerable<LocationModel> GetLastKnownLocations();
    }
}