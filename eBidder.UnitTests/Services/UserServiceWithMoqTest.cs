using eBidder.Services;
using eBidder.UnitTests.Mocks;
using NUnit.Framework;

namespace eBidder.UnitTests.Services
{
    [TestFixture(Category = "UserService tests with Moq")]
    public class UserServiceWithMoqTest
    {
        string username = "stefan";
        string password = "password";
        string newPassword = "newPassword";

        private UserService CreateUserService()
        {
            var fakeRepository = new UserRepositoryMock().CreateRepository();

            var userService = new UserService(fakeRepository);

            return userService;
        }

        [Test]
        public void GivenEmptyRepo_WhenUserCreatedWithUserNameAndPassword_ThenUserIsStoredInRepo()
        {
            // Arrange
            UserService userService = CreateUserService();

            // Act
            var result = userService.CreateUser(username, password);

            // Assert
            Assert.AreEqual(username, result.Username);
            Assert.AreEqual(password, result.Password);
        }

        [Test]
        public void GivenEmptyRepo_WhenGetUserByName_ThenUserNotExist()
        {
            // Arrange
            UserService userService = CreateUserService();

            // Act
            var result = userService.GetByUsername(username);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GivenUser_WhenPasswordChanged_ThenPasswordHasNewValue()
        {
            // Arrange
            var userService = CreateUserService();
            
            // Act
            var user = userService.CreateUser(username, password);
            user = userService.ChangePassword(username, password, newPassword);

            // Assert
            Assert.AreEqual(newPassword, user.Password);
        }

        [Test]
        public void GivenUser_WhenGetByUsername_ThenUserExists()
        {
            // Arrange
            var userService = CreateUserService();

            // Act
            userService.CreateUser(username, password);
            var user = userService.GetByUsername(username);

            // Assert
            Assert.NotNull(user);
        }

        [Test]
        public void GivenEmptyRepo_WhenChangePasswordForNonExistingUser_ThenExceptionIsThrown()
        {
            // Arrange
            UserService userService = CreateUserService();

            // Act and assert together (check below for solution in these situations)
            Assert.That(() => userService.ChangePassword(username, password, newPassword), Throws.InvalidOperationException);
        }

        [Test]
        public void GivenUser_WhenChangePasswordWithWrongOldPassword_ThenExceptionIsThrown()
        {
            // Arrange
            UserService userService = CreateUserService();
            var user = userService.CreateUser(username, password);

            // Act
            // C# offers feature that's called "Local functions" and it allows us to create function within function
            // We can use this advantage to separate Act and Assert parts in our tests, like here
            void FunctionToAssert()
            {
                userService.ChangePassword(username, "wrongPass", newPassword);
            }

            // Assert
            Assert.That(() => FunctionToAssert(), Throws.ArgumentException);
        }

        [Test]
        public void GivenUser_WhenGetUsers_ThenListIsNotEmpty()
        {
            // Arrange
            UserService userService = CreateUserService();
            userService.CreateUser(username, password);

            // Act
            var result = userService.GetUsers();

            // Assert
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GivenEmptyRepo_WhenGetUsers_ThenListIsEmpty()
        {
            // Arrange
            UserService userService = CreateUserService();

            // Act
            var result = userService.GetUsers();

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void GivenUser_WhenDeleteUser_ThenUserIsRemoved()
        {
            // Arrange
            UserService userService = CreateUserService();
            userService.CreateUser(username, password);

            // Act
            var result = userService.DeleteUser(username);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GivenEmptyRepo_WhenDeleteUser_ThenUserIsNotFound()
        {
            // Arrange
            UserService userService = CreateUserService();

            // Act
            var result = userService.DeleteUser(username);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GivenUser_WhenCreateUserWithSameUsername_ThenExceptionIsThrown()
        {
            // Arrange
            var userService = CreateUserService();
            userService.CreateUser(username, password);

            // TODO: Apply "Local functions" feature here to separate Act and Assert 
            Assert.That(() => userService.CreateUser(username, password), Throws.InvalidOperationException);
        }
    }
}