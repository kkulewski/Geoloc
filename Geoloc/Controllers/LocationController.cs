using System.Collections.Generic;
using System.Linq;
using Geoloc.Models;
using Geoloc.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Geoloc.Controllers
{
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private readonly ILocationRepository _repo;

        public LocationController(ILocationRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("[action]")]
        public IActionResult Send([FromBody]LocationWebModel webModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _repo.Add(webModel);
            return new OkObjectResult(JsonConvert.SerializeObject("Location added"));
        }

        [HttpGet("[action]")]
        public IEnumerable<LocationWebModel> Get()
        {
            return _repo.Get();
        }

        [HttpGet("get/last")]
        public IActionResult GetLastKnownLocations()
        {
            var locations = _repo.Get().GroupBy(e => e.UserName).Select(e => e.FirstOrDefault());
            return new OkObjectResult(JsonConvert.SerializeObject(locations));
        }
    }
}