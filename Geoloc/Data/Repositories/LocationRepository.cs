using System;
using System.Collections.Generic;
using System.Linq;
using Geoloc.Data.Repositories.Abstract;
using Geoloc.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Geoloc.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;

        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Location> GetLocationsByUser(Guid userId)
        {
            return _context.Locations
                .Include(x => x.AppUser)
                .Where(location => location.AppUserId == userId);
        }

        public IEnumerable<Location> GetAllLocations()
        {
            return _context.Locations.Include(x => x.AppUser);
        }

        public void Add(Location model)
        {
            _context.Locations.Add(model);
        }
    }
}