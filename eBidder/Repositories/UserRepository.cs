﻿using System;
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

            existingUser.Password = newPassword;
            _context.SaveChanges();

            return existingUser;
        }

        public bool DeleteUser(string username)
        {
            var existingUser = GetUser(username);

            _context.Users.Remove(existingUser);
            _context.SaveChanges();

            return true;
        }

        public void AddMoney(string username, double value)
        {
            var existingUser = GetUser(username);
            existingUser.Money += value;

            _context.SaveChanges();
        }

        public void RemoveMoney(string username, double value)
        {
            var existingUser = GetUser(username);
            existingUser.Money -= value;

            _context.SaveChanges();
        }

        public void TransferMoney(string fromUser, string toUser, double value)
        {
            var userFrom = GetUser(fromUser);
            userFrom.Money -= value;

            var userTo = GetUser(toUser);
            userTo.Money += value;

            _context.SaveChanges();
        }

        public double GetMoney(string username)
        {
            return GetUser(username).Money;
        }


        private User GetUser(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}