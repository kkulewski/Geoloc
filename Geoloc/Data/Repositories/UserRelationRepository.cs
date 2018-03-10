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
        public IEnumerable<UserRelation> GetUserRelations(Guid userId)
        {
            return _context.UserRelations
                .Include(x => x.InvitingUser)
                .Include(x => x.InvitedUser)
                .Where(x => x.InvitingUserId == userId || x.InvitedUserId == userId && x.UserRelationStatus == UserRelationStatus.Friends);
        }

        public IEnumerable<UserRelation> GetUserSentRequests(Guid userId)
        {
            return _context.UserRelations
                .Include(x => x.InvitingUser)
                .Include(x => x.InvitedUser)
                .Where(x => x.InvitingUser.Id == userId && x.UserRelationStatus == UserRelationStatus.Pending);
        }

        public IEnumerable<UserRelation> GetUserReceivedRequests(Guid userId)
        {
            return _context.UserRelations
                .Include(x => x.InvitingUser)
                .Include(x => x.InvitedUser)
                .Where(x => x.InvitedUser.Id == userId && x.UserRelationStatus == UserRelationStatus.Pending);
        }
    }
}