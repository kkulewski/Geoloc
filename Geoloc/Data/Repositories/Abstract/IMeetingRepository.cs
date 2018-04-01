using System;
using System.Collections.Generic;
using Geoloc.Data.Entities;

namespace Geoloc.Data.Repositories.Abstract
{
    public interface IMeetingRepository
    {
        Meeting Get(Guid id);
        void Add(Meeting model);
        IEnumerable<Meeting> GetAll();
    }
}
