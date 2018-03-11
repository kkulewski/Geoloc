using System;
using AutoMapper;
using Geoloc.Data.Repositories.Abstract;
using Geoloc.Models;
using Geoloc.Services.Abstract;

namespace Geoloc.Services
{
    public class UserService : IUserService
    {
        private readonly IAppUserRepository _appUserRepository;

        public UserService(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public UserModel GetById(Guid id)
        {
            var user = _appUserRepository.Get(id);
            var result = Mapper.Map<UserModel>(user);
            return result;
        }

        public UserModel GetByUserName(string userName)
        {
            var user = _appUserRepository.Get(userName);
            var result = Mapper.Map<UserModel>(user);
            return result;
        }
    }
}
