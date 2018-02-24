using System;
using Geoloc.Models.Entities;

namespace Geoloc.Data.Repositories.Abstract
{
    public interface IAppUserRepository
    {
        AppUser Get(Guid id);
    }
}