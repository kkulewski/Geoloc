using System;
using Geoloc.Data.Entities;

namespace Geoloc.Data.Repositories.Abstract
{
    public interface IEventRepository
    {
        Event Get(Guid id);
        void Add(Event model);
    }
}
