using System;
using eBidder.Services;
using eBidder.UnitTests.Mocks;
using NUnit.Framework;

namespace eBidder.UnitTests.Services
{
    public class WalletServiceTest
    {
        private IWalletService _walletService;
        private IUserService _userService;

        [SetUp]
        public void Init()
        {
            var fakeUserRepository = new UserRepositoryMock().CreateRepository();

            _userService = new UserService(fakeUserRepository);
            _walletService = new WalletService(fakeUserRepository);
        }

        [Test]
        public void GivenUser_WhenAddMoney_ThenMoneyIncreased()
        {
            // Arrange 
            var user = _userService.CreateUser("Zoran", "123456");
            // Act

            _walletService.AddMoney("Zoran", 100);

            // Assert
            var actual = _walletService.GetMoney("Zoran");
            Assert.AreEqual(100, actual);
        }

        [Test]
        public void GivenNullUsername_WhenAddMoney_ThenArgumentNullExceptionIsThrown()
        {
            // AAA
            Assert.Throws<ArgumentNullException>(() =>_walletService.AddMoney(null, 1000));
        }

        [Test]
        public void GivenUsernameOfNonExistingUser_WhenAddMoneyIsCalled_ThenInvalidOperationExceptionIsThrown()
        {
            // AAA
            Assert.Throws<InvalidOperationException>(() => _walletService.AddMoney("Bojan", 1000));
        }

        [Test]
        public void GivenUsernameForExistingUser_WhenAddingNegativeAmount_ThenInvalidOperationExceptionIsThorwn()
        {
            // Arrange 
            var user = _userService.CreateUser("Zoran", "123456");
            
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _walletService.AddMoney("Zoran", -100));
        }

        [Test]
        public void GivenUserWithSufficientMoney_WhenRemoveMoney_ThenMoneyDecreased()
        {
            // Arrange 
            var user = _userService.CreateUser("Zoran", "123456");
            // Act

            _walletService.AddMoney("Zoran", 100);
            _walletService.RemoveMoney("Zoran", 50);

            // Assert
            var actual = _walletService.GetMoney("Zoran");
            Assert.AreEqual(50, actual);
        }
    }
}