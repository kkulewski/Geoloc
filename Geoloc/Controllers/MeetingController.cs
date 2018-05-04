using System;
using System.Collections.Generic;
using AutoMapper;
using Geoloc.Models;
using Geoloc.Models.WebModels;
using Geoloc.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Geoloc.Controllers
{
    [Route("api/[controller]"), Authorize]
    public class MeetingController : Controller
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
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
        public IActionResult Create([FromBody] MeetingWebModel webModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = Mapper.Map<MeetingModel>(webModel);

            var isSuccess = _meetingService.AddMeeting(model);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return new OkObjectResult(JsonConvert.SerializeObject("Meeting added"));
        }

        [HttpPost("[action]")]
        public IActionResult Join([FromBody] JoinMeetingRequestWebModel webModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (_meetingService.JoinMeetingAsUser(webModel.UserId, webModel.MeetingId))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
