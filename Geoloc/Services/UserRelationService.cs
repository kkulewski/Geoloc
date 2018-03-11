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

        public void AddRelationRequest(UserRelationModel model)
        {
            // TODO: check if relation already exists to avoid duplicates
            var relation = Mapper.Map<UserRelation>(model);
            _userRelationRepository.Add(relation);
            _unitOfWork.Save();
        }

        public void AcceptRelationRequest(Guid relationId)
        {
            var relation = _userRelationRepository.GetUserRelationById(relationId);
            relation.UserRelationStatus = UserRelationStatus.Accepted;
            _userRelationRepository.Update(relation);
            _unitOfWork.Save();
        }

        public UserRelationModel GetUserRelationById(Guid relationId)
        {
            try
            {
                var relation = _userRelationRepository.GetUserRelationById(relationId);
                var result = Mapper.Map<UserRelationModel>(relation);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<UserRelationModel> GetUserRelations(Guid userId)
        {
            try
            {
                var relations = _userRelationRepository
                    .GetUserRelationsByUser(userId)
                    .ToList()
                    .Where(x => x.InvitingUserId == userId ||
                                x.InvitedUserId == userId &&
                                x.UserRelationStatus == UserRelationStatus.Accepted);

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
                    .Where(x => x.InvitingUser.Id == userId &&
                                x.UserRelationStatus == UserRelationStatus.Pending);

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
                    .Where(x => x.InvitedUser.Id == userId &&
                                x.UserRelationStatus == UserRelationStatus.Pending);

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
