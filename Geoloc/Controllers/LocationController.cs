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
        public IActionResult Send([FromBody]LocationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _repo.Add(model);
            return new OkObjectResult(JsonConvert.SerializeObject("Location added"));
        }

        [HttpGet("[action]")]
        public IEnumerable<LocationModel> Get()
        {
            return _repo.Get();
        }

        [HttpGet("get/last")]
        public IEnumerable<LocationModel> GetLastKnownLocations()
        {
            return _repo.Get().GroupBy(e => e.UserName).Select(e => e.FirstOrDefault());
        }
    }
}