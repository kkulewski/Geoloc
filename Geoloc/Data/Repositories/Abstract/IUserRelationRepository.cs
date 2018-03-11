using System;
using System.Collections.Generic;
using Geoloc.Data.Entities;

namespace Geoloc.Data.Repositories.Abstract
{
    public interface IUserRelationRepository
    {
        IEnumerable<UserRelation> GetUserRelationsByUser(Guid userId);

        UserRelation GetUserRelationById(Guid relationId);

        void Add(UserRelation model);

        void Update(UserRelation model);
    }
}