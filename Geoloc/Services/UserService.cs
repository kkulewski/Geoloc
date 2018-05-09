using System;
using System.Collections.Generic;
using AutoMapper;
using Geoloc.Data.Repositories.Abstract;
using Geoloc.Models;
using Geoloc.Services.Abstract;

namespace Geoloc.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel GetById(Guid id)
        {
            var user = _userRepository.Get(id);
            var result = Mapper.Map<UserModel>(user);
            return result;
        }

        public UserModel GetByUserName(string userName)
        {
            var user = _userRepository.Get(userName);
            var result = Mapper.Map<UserModel>(user);
            return result;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            var result = Mapper.Map<IEnumerable<UserModel>>(users);
            return result;
        }
    }
}
