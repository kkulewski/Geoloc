using System;
using System.Linq;
using Geoloc.Data.Entities;
using Geoloc.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Geoloc.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Event Get(Guid id)
        {
            return _context.Events
                .Include(x => x.Location)
                .Include(x => x.ParticipantUsers)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Add(Event model)
        {
            _context.Events.Add(model);
        }
    }
}
