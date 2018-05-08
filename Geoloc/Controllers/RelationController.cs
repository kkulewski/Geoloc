using System;
using System.Collections.Generic;
using AutoMapper;
using Geoloc.Data.Entities;
using Geoloc.Models;
using Geoloc.Models.WebModels;
using Geoloc.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Geoloc.Controllers
{
    [Route("api/[controller]"), Authorize]
    public class RelationController : Controller
    {
        private readonly IRelationService _relationService;
        private readonly IUserService _userService;

        public RelationController(IRelationService relationService, IUserService userService)
        {
            _relationService = relationService;
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public IEnumerable<RelationWebModel> Relations(Guid userId)
        {
            var model = _relationService.GetUserRelations(userId);
            var result = Mapper.Map<IEnumerable<RelationWebModel>>(model);
            return result;
        }

        [HttpGet("sent/{userId}")]
        public IEnumerable<RelationWebModel> GetSentRequests(Guid userId)
        {
            var model = _relationService.GetUserSentRequests(userId);
            var result = Mapper.Map<IEnumerable<RelationWebModel>>(model);
            return result;
        }

        [HttpGet("received/{userId}")]
        public IEnumerable<RelationWebModel> GetReceivedRequests(Guid userId)
        {
            var model = _relationService.GetUserReceivedRequests(userId);
            var result = Mapper.Map<IEnumerable<RelationWebModel>>(model);
            return result;
        }

        [HttpPost("[action]")]
        public IActionResult Send([FromBody] RelationWebModel webModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var invitingUser = _userService.GetByUserName(webModel.InvitingUserName);
            if (invitingUser == null)
            {
                return BadRequest();
            }

            var invitedUser = _userService.GetByUserName(webModel.InvitedUserName);
            if (invitedUser == null)
            {
                return BadRequest();
            }

            var model = new RelationModel
            {
                RelationStatus = RelationStatus.Pending,
                InvitingUser = invitingUser,
                InvitedUser = invitedUser
            };

            var isSuccess = _relationService.SendRequest(model);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return new OkObjectResult(JsonConvert.SerializeObject("Relation request sent."));
        }

        [HttpPost("[action]")]
        public IActionResult Accept([FromBody] Guid id)
        {
            var model = _relationService.GetRelationById(id);
            if (model == null)
            {
                return BadRequest();
            }

            var isSuccess = _relationService.AcceptRequest(id);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return new OkObjectResult(JsonConvert.SerializeObject("Relation request accepted."));
        }
    }
}
