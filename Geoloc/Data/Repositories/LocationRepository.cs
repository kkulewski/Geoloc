using System.Collections.Generic;
using System.Linq;
using Geoloc.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Geoloc.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(context);
        }

        public void Add(Location model)
        {
            _context.Locations.Add(model);
            _unitOfWork.Save();
        }

        public IEnumerable<Location> GetByUser(string userId)
        {
            return _context.Locations
                .Include(x => x.AppUser)
                .Where(location => location.AppUser.Id == userId);
        }

        public IEnumerable<Location> GetAllLocations()
        {
            return _context.Locations.Include(x => x.AppUser);
        }
    }
}