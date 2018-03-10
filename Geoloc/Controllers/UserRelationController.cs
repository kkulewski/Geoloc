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
        private readonly IUserRelationRepository _userRelationRepository;

        public UserRelationController(IUserRelationRepository userRelationRepository)
        {
            _userRelationRepository = userRelationRepository;
        }

        [HttpGet("[action]/{userId}")]
        public IEnumerable<UserRelationWebModel> Get(Guid userId)
        {
            var model = _userRelationRepository.GetUserRelations(userId);
            var result = Mapper.Map<IEnumerable<UserRelationWebModel>>(model);
            return result;
        }
    }
}