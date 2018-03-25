using System;
using System.Linq;
using AutoMapper;
using Geoloc.Models;
using Geoloc.Models.WebModels;
using Geoloc.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Geoloc.Controllers
{
    [Route("api/[controller]")]
    public class MeetingController : Controller
    {
        private readonly IMeetingService _meetingService;
        private readonly ILocationService _locationService;
        private readonly IUserService _userService;

        public MeetingController(IMeetingService meetingService, ILocationService locationService, IUserService userService)
        {
            _meetingService = meetingService;
            _locationService = locationService;
            _userService = userService;
        }

        [HttpGet("[action]/{meetingId}")]
        public MeetingWebModel Get(Guid meetingId)
        {
            var model = _meetingService.GetById(meetingId);
            var result = Mapper.Map<MeetingWebModel>(model);
            return result;
        }

        [HttpPost("[action]")]
        public IActionResult Send([FromBody]MeetingWebModel webModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = new MeetingModel
            {
                // TODO: handle location, user and time inputs
                Name = webModel.Name,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now + TimeSpan.FromHours(1),
                Location = _locationService.GetLastKnownLocations().FirstOrDefault(),
                User = _userService.GetAllUsers().FirstOrDefault()
            };

            var isSuccess = _meetingService.AddMeeting(model);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return new OkObjectResult(JsonConvert.SerializeObject("Meeting added"));
        }
    }
}
