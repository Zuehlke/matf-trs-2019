using System;
using System.Collections.Generic;
using System.Linq;
using eBidder.Domain;
using eBidder.Repositories;

namespace eBidder.UnitTests.Fakes
{
    public class UserRepositoryFake : IUserRepository
    {
        private ICollection<User> _users = new List<User>();

        public User ChangePassword(string username, string oldPassword, string newPassword)
        {
            var user = GetUser(username);

            if (user == null)
            {
                throw new InvalidOperationException($"User {username} doesn't exist.");
            }

            if (!user.Password.Equals(oldPassword))
            {
                throw new ArgumentException("Wrong password for the user");
            }

            user.Password = newPassword;

            return user;
        }

        public User CreateUser(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = password
            };

            _users.Add(user);

            return user;
        }

        public bool DeleteUser(string username)
        {
            var user = GetUser(username);
            if (user == null)
            {
                return false;
            }

            return _users.Remove(user);
        }

        public void AddMoney(string username, double value)
        {
            throw new NotImplementedException();
        }

        public void RemoveMoney(string username, double value)
        {
            throw new NotImplementedException();
        }

        public double GetMoney(string username)
        {
            throw new NotImplementedException();
        }

        public User GetByUsername(string username)
        {
            return GetUser(username);
        }

        public IEnumerable<User> GetUsers()
        {
            return _users;
        }

        private User GetUser(string username)
        {
            return _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}