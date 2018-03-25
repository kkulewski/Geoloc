using System;
using Geoloc.Models;

namespace Geoloc.Services.Abstract
{
    public interface IUserService
    {
        UserModel GetById(Guid userId);
        UserModel GetByUserName(string userName);
    }
}
