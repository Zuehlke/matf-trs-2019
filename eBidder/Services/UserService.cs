using System.Collections.Generic;
using System.Linq;
using eBidder.Mappers;
using eBidder.Models;
using eBidder.Repositories;

namespace eBidder.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            var unitOfWork = new UnitOfWork();
            _userRepository = unitOfWork.UserRepository;
        }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            return _userRepository.GetUsers().Select(user => user?.ToUserViewModel());
        }

        public UserViewModel GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username)?.ToUserViewModel();
        }

        public UserViewModel CreateUser(string username, string password)
        {
            return _userRepository.CreateUser(username, password).ToUserViewModel();
        }

        public bool DeleteUser(string username)
        {
            return _userRepository.DeleteUser(username);
        }

        public UserViewModel ChangePassword(string username, string oldPassword, string newPassword)
        {
            return _userRepository.ChangePassword(username, oldPassword, newPassword).ToUserViewModel();
        }
    }
}