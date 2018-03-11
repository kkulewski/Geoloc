using System;
using System.Collections.Generic;
using System.Linq;
using Geoloc.Data.Entities;
using Geoloc.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Geoloc.Data.Repositories
{
    class UserRelationRepository : IUserRelationRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRelationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserRelation> GetUserRelationsByUser(Guid userId)
        {
            return _context.UserRelations
                .Include(x => x.InvitingUser)
                .Include(x => x.InvitedUser)
                .Where(x => x.InvitingUserId == userId || x.InvitedUserId == userId);
        }

        public UserRelation GetUserRelationById(Guid relationId)
        {
            return _context.UserRelations
                .Include(x => x.InvitingUser)
                .Include(x => x.InvitedUser)
                .FirstOrDefault(x => x.Id == relationId);
        }

        public void Add(UserRelation model)
        {
            _context.UserRelations.Add(model);
        }

        public void Update(UserRelation model)
        {
            _context.UserRelations.Update(model);
        }
    }
}