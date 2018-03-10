using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Geoloc.Data;
using Geoloc.Data.Entities;
using Geoloc.Data.Repositories.Abstract;
using Geoloc.Models;
using Geoloc.Services.Abstract;

namespace Geoloc.Services
{
    public class UserRelationService : IUserRelationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRelationRepository _userRelationRepository;

        public UserRelationService(IUnitOfWork unitOfWork, IUserRelationRepository userRelationRepository)
        {
            _unitOfWork = unitOfWork;
            _userRelationRepository = userRelationRepository;
        }

        public IEnumerable<UserRelationModel> GetUserRelations(Guid userId)
        {
            try
            {
                var relations = _userRelationRepository
                    .GetUserRelationsByUser(userId)
                    .ToList()
                    .Where(x => x.InvitingUserId == userId ||
                                x.InvitedUserId == userId && x.UserRelationStatus == UserRelationStatus.Friends);

                var result = Mapper.Map<IEnumerable<UserRelationModel>>(relations);
                return result;
            }
            catch (Exception)
            {
                return new List<UserRelationModel>();
            }
        }

        public IEnumerable<UserRelationModel> GetUserSentRequests(Guid userId)
        {
            try
            {
                var relations = _userRelationRepository
                    .GetUserRelationsByUser(userId)
                    .ToList()
                    .Where(x => x.InvitingUser.Id == userId && x.UserRelationStatus == UserRelationStatus.Pending);

                var result = Mapper.Map<IEnumerable<UserRelationModel>>(relations);
                return result;
            }
            catch (Exception)
            {
                return new List<UserRelationModel>();
            }
        }

        public IEnumerable<UserRelationModel> GetUserReceivedRequests(Guid userId)
        {
            try
            {
                var relations = _userRelationRepository
                    .GetUserRelationsByUser(userId)
                    .ToList()
                    .Where(x => x.InvitedUser.Id == userId && x.UserRelationStatus == UserRelationStatus.Pending);

                var result = Mapper.Map<IEnumerable<UserRelationModel>>(relations);
                return result;
            }
            catch (Exception)
            {
                return new List<UserRelationModel>();
            }
        }
    }
}
