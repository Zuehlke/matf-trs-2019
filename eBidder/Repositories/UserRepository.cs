using System;
using System.Collections.Generic;
using System.Linq;
using eBidder.Domain;

namespace eBidder.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetByUsername(string username)
        {
            return GetUser(username);
        }

        public User CreateUser(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = password
            };

            var newUser = _context.Users.Add(user);
            _context.SaveChanges();

            return newUser;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User ChangePassword(string username, string oldPassword, string newPassword)
        {
            var existingUser = GetUser(username);

            if (existingUser == null)
            {
                throw new InvalidOperationException($"User {username} doesn't exist.");
            }
            if (!existingUser.Password.Equals(oldPassword))
            {
                throw new ArgumentException("Wrong password for the user");
            }

            existingUser.Password = newPassword;
            _context.SaveChanges();

            return existingUser;
        }

        public bool DeleteUser(string username)
        {
            User existingUser = GetUser(username);

            if (existingUser == null)
            {
                return false;
            }

            _context.Users.Remove(existingUser);
            _context.SaveChanges();

            return true;
        }

        private User GetUser(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}