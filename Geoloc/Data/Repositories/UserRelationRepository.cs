using System;
using System.Collections.Generic;
using System.Linq;
using Geoloc.Data.Repositories.Abstract;
using Geoloc.Models.Entities;

namespace Geoloc.Data.Repositories
{
    class UserRelationRepository : IUserRelationRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRelationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<UserRelation> GetGivenUserRelations(Guid userId)
        {
            return _context.UserRelations.Where(x => x.FirstAppUserId == userId || x.SecondAppUserId == userId);
        }
    }
}