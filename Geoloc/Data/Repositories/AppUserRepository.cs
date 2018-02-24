using System.Linq;
using Geoloc.Data.Repositories.Abstract;
using Geoloc.Models.Entities;

namespace Geoloc.Data.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public AppUserRepository(ApplicationDbContext context)
        {
            _unitOfWork = new UnitOfWork(_context);
            _context = context;
        }

        public AppUser Get(string id)
        {
            return _context.Users.SingleOrDefault(user => user.Id == id);
        }
    }
}