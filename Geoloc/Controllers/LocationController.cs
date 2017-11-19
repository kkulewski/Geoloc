using System.Collections.Generic;
using Geoloc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Geoloc.Controllers
{
    public static class LocationRepository
    {
        public static IList<LocationModel> Locations = new List<LocationModel>();
    }

    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        [HttpPost("[action]")]
        public void Send([FromBody]LocationModel model)
        {
            LocationRepository.Locations.Add(model);
        }

        [HttpGet("[action]")]
        public IEnumerable<LocationModel> Get()
        {
            return LocationRepository.Locations;
        }

    }
}