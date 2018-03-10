using System;
using System.Collections.Generic;
using System.Linq;
using Geoloc.Data.Entities;
using Geoloc.Data.Repositories.Abstract;

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
            return _context.UserRelations.Where(x => x.InvitingUserId == userId || x.InvitedUserId == userId);
        }
    }
}