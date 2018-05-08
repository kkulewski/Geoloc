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

        public bool SendRelationRequest(UserRelationModel model)
        {
            try
            {
                var userId = model.InvitingUser.Id;
                var userRelations = _userRelationRepository.GetUserRelationsByUser(userId);

                var relationExists = userRelations.Any(x =>
                    x.InvitingUser.Id == userId && x.InvitedUser.Id == model.InvitedUser.Id ||
                    x.InvitedUser.Id == userId && x.InvitingUser.Id == model.InvitedUser.Id);

                var selfInvitation = model.InvitingUser.Id == model.InvitedUser.Id;

                if (relationExists || selfInvitation)
                {
                    return false;
                }

                var relation = Mapper.Map<UserRelation>(model);
                relation.UserRelationStatus = UserRelationStatus.Pending;
                _userRelationRepository.Add(relation);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                // TODO: error handling
                return false;
            }
        }

        public bool AcceptRelationRequest(Guid relationId)
        {
            try
            {
                var relation = _userRelationRepository.GetUserRelationById(relationId);
                if (relation.UserRelationStatus == UserRelationStatus.Accepted)
                {
                    return false;
                }

                relation.UserRelationStatus = UserRelationStatus.Accepted;
                _userRelationRepository.Update(relation);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                // TODO: error handling
                return false;
            }
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
                    .Where(x => (x.InvitingUser.Id == userId && x.UserRelationStatus == UserRelationStatus.Accepted) ||
                                (x.InvitedUser.Id == userId && x.UserRelationStatus == UserRelationStatus.Accepted));

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
