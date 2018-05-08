using System;
using System.Collections.Generic;
using System.Linq;
using Geoloc.Data.Entities;
using Geoloc.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Geoloc.Data.Repositories
{
    class RelationRepository : IRelationRepository
    {
        private readonly ApplicationDbContext _context;

        public RelationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Relation> GetRelationsByUserId(Guid userId)
        {
            return _context.Relations
                .Include(x => x.InvitingUser)
                .Include(x => x.InvitedUser)
                .Where(x => x.InvitingUserId == userId || x.InvitedUserId == userId);
        }

        public Relation GetRelationById(Guid relationId)
        {
            return _context.Relations
                .Include(x => x.InvitingUser)
                .Include(x => x.InvitedUser)
                .FirstOrDefault(x => x.Id == relationId);
        }

        public void Add(Relation model)
        {
            _context.Relations.Add(model);
        }

        public void Update(Relation model)
        {
            _context.Relations.Update(model);
        }
    }
}
