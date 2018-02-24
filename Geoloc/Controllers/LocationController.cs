using System;
using System.Collections.Generic;
using AutoMapper;
using Geoloc.Models;
using Geoloc.Models.WebModels;
using Geoloc.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Geoloc.Controllers
{
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost("[action]")]
        public IActionResult Send([FromBody]LocationWebModel webModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = Mapper.Map<LocationModel>(webModel);

            _locationService.AddLocation(model);

            return new OkObjectResult(JsonConvert.SerializeObject("Location added"));
        }

        [HttpGet("[action]/{userId}")]
        public IEnumerable<LocationWebModel> Get(Guid userId)
        {
            var model = _locationService.GetLocationByUserId(userId);
            var result = Mapper.Map<IEnumerable<LocationWebModel>>(model);
            return result;
        }

        [HttpGet("get/last")]
        public IActionResult GetLastKnownLocations()
        {
            var model = _locationService.GetLastKnownLocations();
            var result = Mapper.Map<IEnumerable<LocationWebModel>>(model);
            return new OkObjectResult(result);
        }
    }
}