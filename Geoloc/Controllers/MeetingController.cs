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

        [HttpGet]
        public IEnumerable<MeetingWebModel> Get()
        {
            var model = _meetingService.GetAllMeetings();
            var result = Mapper.Map<IEnumerable<MeetingWebModel>>(model);
            return result;
        }

        [HttpGet("{meetingId}")]
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

            var location = _locationService.GetById(webModel.LocationId);
            if (location == null)
            {
                return BadRequest();
            }

            var user = _userService.GetById(webModel.UserId);
            if (user == null)
            {
                return BadRequest();
            }

            var model = new MeetingModel
            {
                // TODO: handle time inputs
                Name = webModel.Name,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now + TimeSpan.FromHours(1),
                Location = location,
                User = user
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
