using System;
using System.Collections.Generic;
using Geoloc.Data.Entities;

namespace Geoloc.Data.Repositories.Abstract
{
    public interface IUserRelationRepository
    {
        IEnumerable<UserRelation> GetUserRelations(Guid userId);

        IEnumerable<UserRelation> GetUserSentRequests(Guid userId);

        IEnumerable<UserRelation> GetUserReceivedRequests(Guid userId);
    }
}