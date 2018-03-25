using System;
using Geoloc.Data.Entities;

namespace Geoloc.Data.Repositories.Abstract
{
    public interface IMeetingRepository
    {
        Meeting Get(Guid id);
        void Add(Meeting model);
    }
}
