using System;
using AutoMapper;
using Geoloc.Models.WebModels;
using Geoloc.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Geoloc.Controllers
{
    [Route("api/[controller]")]
    public class MeetingController : Controller
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpGet("[action]/{meetingId}")]
        public MeetingWebModel Get(Guid meetingId)
        {
            var model = _meetingService.GetById(meetingId);
            var result = Mapper.Map<MeetingWebModel>(model);
            return result;
        }
    }
}