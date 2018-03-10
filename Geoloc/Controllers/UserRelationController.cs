using System;
using System.Collections.Generic;
using AutoMapper;
using Geoloc.Data.Repositories.Abstract;
using Geoloc.Models;
using Geoloc.Models.WebModels;
using Geoloc.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Geoloc.Controllers
{
    [Route("api/[controller]")]
    public class UserRelationController : Controller
    {
        private readonly IUserRelationService _userRelationService;

        public UserRelationController(IUserRelationService userRelationService)
        {
            _userRelationService = userRelationService;
        }

        [HttpGet("{userId}")]
        public IEnumerable<UserRelationWebModel> Relations(Guid userId)
        {
            var model = _userRelationService.GetUserRelations(userId);
            var result = Mapper.Map<IEnumerable<UserRelationWebModel>>(model);
            return result;
        }

        [HttpGet("sent/{userId}")]
        public IEnumerable<UserRelationWebModel> GetSentRequests(Guid userId)
        {
            var model = _userRelationService.GetUserSentRequests(userId);
            var result = Mapper.Map<IEnumerable<UserRelationWebModel>>(model);
            return result;
        }

        [HttpGet("received/{userId}")]
        public IEnumerable<UserRelationWebModel> GetReceivedRelations(Guid userId)
        {
            var model = _userRelationService.GetUserReceivedRequests(userId);
            var result = Mapper.Map<IEnumerable<UserRelationWebModel>>(model);
            return result;
        }
    }
}