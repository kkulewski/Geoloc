using System;
using System.Collections.Generic;
using System.Linq;
using Geoloc.Data.Entities;
using Geoloc.Data.Repositories.Abstract;

namespace Geoloc.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User Get(Guid id)
        {
            return _context.Users.SingleOrDefault(user => user.Id == id);
        }

        public User Get(string userName)
        {
            return _context.Users.SingleOrDefault(user => user.UserName == userName);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }
    }
}
