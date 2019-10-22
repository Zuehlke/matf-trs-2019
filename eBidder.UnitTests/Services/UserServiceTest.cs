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
            Assert.Fail();
        }

        [Test]
        public void GivenUser_WhenPasswordChanged_ThenPasswordHasNewValue()
        {
            Assert.Fail();
        }

        [Test]
        public void GetByExistingUserNameTest()
        {
            Assert.Fail();
        }

        [Test]
        public void ChangePasswordForNonExistingUser()
        {
            Assert.Fail();
        }

        [Test]
        public void ChangePasswordWithWrongOldPasswords()
        {
            Assert.Fail();
        }
    }
}