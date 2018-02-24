using System;
using System.Collections.Generic;
using System.Linq;
using Geoloc.Data.Entities;
using Geoloc.Data.Repositories.Abstract;

namespace Geoloc.Data.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly ApplicationDbContext _context;

        public AppUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public AppUser Get(Guid id)
        {
            return _context.Users.SingleOrDefault(user => user.Id == id);
        }

        public IEnumerable<AppUser> GetAll()
        {
            return _context.Users;
        }
    }
}