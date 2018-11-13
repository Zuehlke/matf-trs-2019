using System.Collections.Generic;
using System.Linq;
using eBidder.Mappers;
using eBidder.Models;
using eBidder.Repositories;
using System;

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
            if (username == null)
            {
                throw new ArgumentNullException("Username must be provided");
            }

            return _userRepository.GetByUsername(username)?.ToUserViewModel();
        }

        public UserViewModel CreateUser(string username, string password)
        {
            if (username == null)
            {
                throw new ArgumentNullException("Username must be provided");
            }

            if (password == null)
            {
                throw new ArgumentNullException("Password must be provided");
            }

            var existingUser = GetUser(username);

            if (existingUser != null)
            {
                throw new InvalidOperationException($"User {username} already exists");
            }

            return _userRepository.CreateUser(username, password).ToUserViewModel();
        }

        public bool DeleteUser(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException("Username must be provided");
            }

            var existingUser = GetUser(username);

            if (existingUser == null)
            {
                return false;
            }

            return _userRepository.DeleteUser(username);
        }

        public UserViewModel ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (username == null)
            {
                throw new ArgumentNullException("Username must be provided");
            }

            if (oldPassword == null)
            {
                throw new ArgumentNullException("OldPassword must be provided");
            }

            if (newPassword == null)
            {
                throw new ArgumentNullException("NewPassword must be provided");
            }

            var existingUser = GetUser(username);
            if (existingUser == null)
            {
                throw new InvalidOperationException($"User {username} doesn't exist");
            }

            if (!existingUser.Password.Equals(oldPassword))
            {
                throw new ArgumentException("Wrong password for the user");
            }

            return _userRepository.ChangePassword(username, oldPassword, newPassword).ToUserViewModel();
        }

        private UserViewModel GetUser(string username)
        {
            return _userRepository.GetByUsername(username)?.ToUserViewModel();
        }
    }
}