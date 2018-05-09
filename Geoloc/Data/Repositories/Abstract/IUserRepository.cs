using System;
using System.Collections.Generic;
using Geoloc.Data.Entities;

namespace Geoloc.Data.Repositories.Abstract
{
    public interface IUserRepository
    {
        User Get(Guid id);
        User Get(string userName);
        IEnumerable<User> GetAll();
    }
}
