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
    public class RelationService : IRelationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRelationRepository _relationRepository;

        public RelationService(IUnitOfWork unitOfWork, IRelationRepository relationRepository)
        {
            _unitOfWork = unitOfWork;
            _relationRepository = relationRepository;
        }

        public bool SendRequest(RelationModel model)
        {
            try
            {
                var userId = model.InvitingUser.Id;
                var userRelations = _relationRepository.GetRelationsByUserId(userId);

                var relationExists = userRelations.Any(x =>
                    x.InvitingUser.Id == userId && x.InvitedUser.Id == model.InvitedUser.Id ||
                    x.InvitedUser.Id == userId && x.InvitingUser.Id == model.InvitedUser.Id);

                var selfInvitation = model.InvitingUser.Id == model.InvitedUser.Id;

                if (relationExists || selfInvitation)
                {
                    return false;
                }

                var relation = Mapper.Map<Relation>(model);
                relation.RelationStatus = RelationStatus.Pending;
                _relationRepository.Add(relation);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AcceptRequest(Guid relationId)
        {
            try
            {
                var relation = _relationRepository.GetRelationById(relationId);
                if (relation.RelationStatus != RelationStatus.Pending)
                {
                    return false;
                }

                relation.RelationStatus = RelationStatus.Accepted;
                _relationRepository.Update(relation);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public RelationModel GetRelationById(Guid relationId)
        {
            try
            {
                var relation = _relationRepository.GetRelationById(relationId);
                var result = Mapper.Map<RelationModel>(relation);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<RelationModel> GetUserRelations(Guid userId)
        {
            try
            {
                var relations = _relationRepository
                    .GetRelationsByUserId(userId)
                    .ToList()
                    .Where(x => (x.InvitingUser.Id == userId && x.RelationStatus == RelationStatus.Accepted) ||
                                (x.InvitedUser.Id == userId && x.RelationStatus == RelationStatus.Accepted));

                var result = Mapper.Map<IEnumerable<RelationModel>>(relations);
                return result;
            }
            catch (Exception)
            {
                return new List<RelationModel>();
            }
        }

        public IEnumerable<RelationModel> GetUserSentRequests(Guid userId)
        {
            try
            {
                var relations = _relationRepository
                    .GetRelationsByUserId(userId)
                    .ToList()
                    .Where(x => x.InvitingUser.Id == userId &&
                                x.RelationStatus == RelationStatus.Pending);

                var result = Mapper.Map<IEnumerable<RelationModel>>(relations);
                return result;
            }
            catch (Exception)
            {
                return new List<RelationModel>();
            }
        }

        public IEnumerable<RelationModel> GetUserReceivedRequests(Guid userId)
        {
            try
            {
                var relations = _relationRepository
                    .GetRelationsByUserId(userId)
                    .ToList()
                    .Where(x => x.InvitedUser.Id == userId &&
                                x.RelationStatus == RelationStatus.Pending);

                var result = Mapper.Map<IEnumerable<RelationModel>>(relations);
                return result;
            }
            catch (Exception)
            {
                return new List<RelationModel>();
            }
        }
    }
}
