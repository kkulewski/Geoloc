using System;
using System.Collections.Generic;
using Geoloc.Data.Entities;

namespace Geoloc.Data.Repositories.Abstract
{
    public interface IRelationRepository
    {
        IEnumerable<Relation> GetRelationsByUserId(Guid userId);
        Relation GetRelationById(Guid relationId);
        void Add(Relation model);
        void Update(Relation model);
    }
}
