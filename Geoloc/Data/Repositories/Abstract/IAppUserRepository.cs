using System;
using Geoloc.Data.Entities;

namespace Geoloc.Data.Repositories.Abstract
{
    public interface IAppUserRepository
    {
        AppUser Get(Guid id);
        AppUser Get(string userName);
    }
}