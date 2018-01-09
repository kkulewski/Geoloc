using System;
using System.Collections.Generic;
using System.Linq;
using Geoloc.Data.Repositories;
using Geoloc.Models;
using Geoloc.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Geoloc.Controllers
{
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private readonly ILocationRepository _locationRepository;

        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpPost("[action]")]
        public IActionResult Send([FromBody]LocationWebModel webModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var model = new Location
            {
                AppUser = new AppUser(),
                Latitude = webModel.Latitude,
                Longitude = webModel.Longitude
            };
            _locationRepository.Add(model);
            return new OkObjectResult(JsonConvert.SerializeObject("Location added"));
        }

        [HttpGet("[action]/{userId}")]
        public IEnumerable<Location> Get(string userId)
        {
            return _locationRepository.GetByUser(userId);
        }

        [HttpGet("get/last")]
        public IActionResult GetLastKnownLocations()
        {
            var locations = _locationRepository.GetAllLocations()
                .GroupBy(e => e.AppUser.Id)
                .Select(e => e.FirstOrDefault());
            var locationWebModels = locations.Select(x => new LocationWebModel
            {
                Longitude = x.Longitude,
                Latitude = x.Latitude,
                Timestamp = DateTime.Now.ToFileTime(),

            });
            return new OkObjectResult(locationWebModels);
        }
    }
}