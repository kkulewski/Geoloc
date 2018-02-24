using System;
using System.Collections.Generic;
using Geoloc.Models.Entities;

namespace Geoloc.Data.Repositories.Abstract
{
    public interface IUserRelationRepository
    {
        IEnumerable<UserRelation> GetGivenUserRelations(Guid userId);
    }
}