using System;
using System.Linq;
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
            Assert.Throws<InvalidOperationException>(() => userService.ChangePassword("aleksandar", "test123", "test456"));
        }

        [Test]
        public void ChangePasswordWithWrongOldPasswords()
        {
            // Arrange
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);
            fakeRepository.CreateUser("stefan", "test123");

            // Act and assert
            Assert.Throws<ArgumentException>(() => userService.ChangePassword("stefan", "test", "test456"));
        }

        [Test]
        public void GivenRepositoryWithOneUser_WhenCallingGetUser_UserFromRepositoryIsReturned()
        {
            // Arrange
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);
            fakeRepository.CreateUser("stefan", "test123");
            
            // Act
            var user = userService.GetByUsername("stefan");

            // Assert
            Assert.AreEqual("stefan", user.Username);
        }

        [Test]
        public void GivenEmptyRepository_WhenGetUserIsCalled_NullIsReturned()
        {
            // Arrange
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);

            // Act
            var user = userService.GetByUsername("stefan");

            // Assert
            Assert.IsNull(user);
        }

        [Test]
        public void GivenRepositoryWithASingleUser_WhenDeleteUserIsCalled_GetUsersReturnsEmptyList()
        {
            // Arrange
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);
            fakeRepository.CreateUser("stefan", "test123");
            
            // Act
            userService.DeleteUser("stefan");
            var allUsers = userService.GetUsers();

            // Assert
            Assert.IsEmpty(allUsers);
        }

        [Test]
        public void GivenEmptyRepository_WhenDeleteUserIsCalled_FalseIsReturnedAsSuccessIndicator()
        {
            // Arrange
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);
            
            // Act
            var successfullyDeleted = userService.DeleteUser("stefan");

            // Assert
            Assert.IsFalse(successfullyDeleted);
        }

        [Test]
        public void GivenARepositoryWithOneExistingUser_WhenCreateUserIsCalled_AndUserAlreadyExists_InvalidOperationExceptionIsThrown()
        {
            // Arrange
            var fakeRepository = new UserRepositoryFake();
            var userService = new UserService(fakeRepository);
            userService.CreateUser("stefan", "test123");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => userService.CreateUser("stefan", "test345"));
        }
    }
}