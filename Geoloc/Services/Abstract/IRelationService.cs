using System;
using System.Collections.Generic;
using Geoloc.Models;

namespace Geoloc.Services.Abstract
{
    public interface IRelationService
    {
        bool SendRequest(RelationModel model);
        bool AcceptRequest(Guid relationId);
        RelationModel GetRelationById(Guid relationId);
        IEnumerable<RelationModel> GetUserRelations(Guid userId);
        IEnumerable<RelationModel> GetUserSentRequests(Guid userId);
        IEnumerable<RelationModel> GetUserReceivedRequests(Guid userId);
    }
}
