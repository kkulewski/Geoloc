using System;
using System.Collections.Generic;
using Geoloc.Data.Entities;

namespace Geoloc.Data.Repositories.Abstract
{
    public interface IAppUserRepository
    {
        AppUser Get(Guid id);
        AppUser Get(string userName);
        IEnumerable<AppUser> GetAll();
    }
}
