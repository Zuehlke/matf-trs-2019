using System.Collections.Generic;
using eBidder.Models;

namespace eBidder.Services
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> GetUsers();

        UserViewModel GetByUsername(string username);

        UserViewModel CreateUser(string username, string password);

        bool DeleteUser(string username);

        UserViewModel ChangePassword(string username, string oldPassword, string newPassword);
    }
}