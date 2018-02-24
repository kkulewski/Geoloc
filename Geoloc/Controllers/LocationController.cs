using System;
using System.Collections.Generic;
using System.Linq;
using Geoloc.Data.Repositories;
using Geoloc.Data.Repositories.Abstract;
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
        private readonly IAppUserRepository _appUserRepository;

        public LocationController(ILocationRepository locationRepository, IAppUserRepository appUserRepository)
        {
            _locationRepository = locationRepository;
            _appUserRepository = appUserRepository;
        }

        [HttpPost("[action]")]
        public IActionResult Send([FromBody]LocationWebModel webModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var model = new Location
            {
                AppUser = _appUserRepository.Get(webModel.UserId),
                Latitude = webModel.Latitude,
                Longitude = webModel.Longitude,
                CreatedOn = DateTime.Now
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
                .Select(x => x.OrderByDescending(c => c.CreatedOn).FirstOrDefault());

            var locationWebModels = locations.Select(x => new LocationWebModel
            {
                Longitude = x.Longitude,
                Latitude = x.Latitude,
                Username = x.AppUser.Email,
                Timestamp = DateTime.Now.ToFileTime(),
                UserId = x.AppUser.Id
            });
            return new OkObjectResult(locationWebModels);
        }
    }
}