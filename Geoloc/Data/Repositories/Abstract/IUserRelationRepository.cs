using System;
using System.Collections.Generic;
using Geoloc.Data.Entities;

namespace Geoloc.Data.Repositories.Abstract
{
    public interface IUserRelationRepository
    {
        IEnumerable<UserRelation> GetUserRelationsByUser(Guid userId);
    }
}