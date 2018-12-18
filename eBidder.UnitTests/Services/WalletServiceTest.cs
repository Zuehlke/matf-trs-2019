using System;
using System.Linq;
using eBidder.Domain;
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
            var mockedUserRepository = new UserRepositoryMock().CreateRepository();
            var mockedTransactionLogRepository = new TransactionLogRepositoryMock().CreateRepository();

            _userService = new UserService(mockedUserRepository);
            _walletService = new WalletService(mockedUserRepository, mockedTransactionLogRepository);
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

        [Test]
        public void TEST1()
        {
            Assert.Fail();
        }

        [Test]
        public void TEST2()
        {
            Assert.Fail();
        }

        [Test]
        public void TEST3()
        {
            Assert.Fail();
        }

        [Test]
        public void TEST4()
        {
            Assert.Fail();
        }

        [Test]
        public void TEST5()
        {
            Assert.Fail();
        }
    }
}