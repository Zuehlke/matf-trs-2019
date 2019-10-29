using System;
using eBidder.Repositories;
using eBidder.Services;
using eBidder.UnitTests.Fakes;
using NUnit.Framework;

namespace eBidder.UnitTests.Services
{
    class UserServiceTest
    {
        [Test]
        public void GivenEmptyRepo_WhenUserCreatedWithUserNameAndPassword_ThenUserIsStoredInRepo()
        {
            // Arrange
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);

            // Act
            var result = userService.CreateUser("stefan", "test123");

            // Assert
            Assert.AreEqual("stefan", result.Username);
            Assert.AreEqual("test123", result.Password);
        }

        [Test]
        public void GivenEmptyRepo_WhenGetUserByName_ThenUserNotExist()
        {
            // Arrange
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);

            // Act
            var result = userService.GetByUsername("stefan");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GivenUser_WhenPasswordChanged_ThenPasswordHasNewValue()
        {
            // Arrange
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);
            fakeRepository.CreateUser("stefan", "test123");

            // Act
            var result = userService.ChangePassword("stefan", "test123", "test456");

            // Assert
            Assert.AreEqual("test456", result.Password);
        }

        [Test]
        public void GetByExistingUserNameTest()
        {
            // Arrange
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);
            fakeRepository.CreateUser("stefan", "test123");

            // Act
            var result = userService.GetByUsername("stefan");

            // Assert
            Assert.AreEqual("stefan", result.Username);
        }

        [Test]
        public void ChangePasswordForNonExistingUser()
        {
            // Arrange
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);

            // Act and assert
            Assert.Throws<System.InvalidOperationException>(() => userService.ChangePassword("aleksandar", "test123", "test456"));
        }

        [Test]
        public void ChangePasswordWithWrongOldPasswords()
        {
            // Arrange
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);
            fakeRepository.CreateUser("stefan", "test123");

            // Act and assert
            Assert.Throws<System.ArgumentException>(() => userService.ChangePassword("stefan", "test", "test456"));
        }

        [Test]
        public void GetUser_PositiveTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void GetUser_WhenRepositoryIsEmpty()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void DeleteUser_PositiveTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void DeleteUser_WhenRepositoryIsEmpty()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CreateUser_WhenUserAlreadyExists()
        {
            throw new NotImplementedException();
        }
    }
}