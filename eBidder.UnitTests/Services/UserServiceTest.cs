using System;
using eBidder.Repositories;
using eBidder.Services;
using eBidder.UnitTests.Fakes;
using NUnit.Framework;

namespace eBidder.UnitTests.Services
{
    [Ignore("Not executed on exam!")]
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
        public void GivenUser_WhenGetUsers_ThenListIsNotEmpty()
        {
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);
            userService.CreateUser("stefan", "pass");

            var result = userService.GetUsers();

            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GivenEmptyRepo_WhenGetUsers_ThenListIsEmpty()
        {
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);

            var result = userService.GetUsers();

            Assert.IsEmpty(result);
        }

        [Test]
        public void GivenUser_WhenDeleteUser_ThenUserIsRemoved()
        {
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);
            userService.CreateUser("stefan", "pass");

            var result = userService.DeleteUser("stefan");

            Assert.IsTrue(result);
        }

        [Test]
        public void GivenEmptyRepo_WhenDeleteUser_ThenUserIsNotFound()
        {
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);

            var result = userService.DeleteUser("stefan");

            Assert.IsFalse(result);
        }

        [Test]
        public void GivenUser_WhenCreateUserWithSameUsername_ThenExceptionIsThrown()
        {
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);
            userService.CreateUser("stefan", "pass");

            Assert.That(() => userService.CreateUser("stefan", "pass"), Throws.InvalidOperationException);
        }
    }
}