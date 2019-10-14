using System.Collections.Generic;
using eBidder.Domain;

namespace eBidder.Repositories
{
    public interface IUserRepository
    {
        User GetByUsername(string username);

        User CreateUser(string username, string password);

        IEnumerable<User> GetUsers();

        User ChangePassword(string username, string oldPassword, string newPassword);

        bool DeleteUser(string username);
    }
}