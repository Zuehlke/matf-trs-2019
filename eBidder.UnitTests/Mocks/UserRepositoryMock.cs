using eBidder.Domain;
using eBidder.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eBidder.UnitTests.Mocks
{
    /// <summary>
    /// This is one way of doing things, we could mock the things we need directly in test class/test method
    /// </summary>

    internal class UserRepositoryMock
    {
        ICollection<User> _users;

        public IUserRepository CreateRepository()
        {
            _users = new List<User>();

            var fakeRepository = new Mock<IUserRepository>();

            fakeRepository.Setup(x => x.GetUsers()).Returns(_users);

            fakeRepository.Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>()))
                          .Returns((string username, string password) => new User { Username = username, Password = password })
                          .Callback((string username, string password) => _users.Add(new User { Username = username, Password = password }));

            fakeRepository.Setup(x => x.DeleteUser(It.IsAny<string>()))
                          .Returns((string username) => _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)) != null ? true : false)
                          .Callback((string username) => _users.Remove(_users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase))));

            fakeRepository.Setup(x => x.GetByUsername(It.IsAny<string>()))
                          .Returns((string username) => _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)));

            fakeRepository.Setup(x => x.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                          .Callback((string username, string oldPassword, string newPassword) => (_users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase))).Password = newPassword)
                          .Returns((string username, string oldPassword, string newPassword) => (_users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase))));

            fakeRepository.Setup(x => x.AddMoney(It.IsAny<string>(), It.IsAny<double>()))
                .Callback((string username, double money) =>
                    _users.First(user => user.Username == username).Money += money);

            fakeRepository.Setup(x => x.GetMoney(It.IsAny<string>()))
                .Returns((string username) => _users.First(user => user.Username == username).Money );

            fakeRepository.Setup(x => x.RemoveMoney(It.IsAny<string>(), It.IsAny<double>()))
                .Callback((string username, double money) =>
                    _users.First(user => user.Username == username).Money -= money);
            // TODO (ispit): Add missing methods mock implementation
            fakeRepository.Setup(x => x.TransferMoney(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>()))
                .Callback((string fromUser, string toUser, double amount) =>
                    {
                        _users.FirstOrDefault(user => user.Username == fromUser).Money -= amount;
                        _users.FirstOrDefault(user => user.Username == toUser).Money += amount;
                    });

            return fakeRepository.Object;
        }
    }
}
