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
        public void GivenUser_WhenMoneyTransferWithNegativeAmount_ThenUnsuccessfulMoneyTransferLogged()
        {
            // Arrange
            var david = _userService.CreateUser("David", "123456");
            var zoran = _userService.CreateUser("Zoran", "1231245");

            // Act
            Assert.Throws<InvalidOperationException>(() => _walletService.Transfer(david.Username, zoran.Username, -100));
            var transactionLogs = _walletService.GetTransactionLogs().ToList();

            // Assert
            Assert.AreEqual(1, transactionLogs.Count());
            var transactionLog = transactionLogs.FirstOrDefault();

            Assert.AreEqual(TransactionStatus.Unsuccessful, transactionLog.TransactionStatus);
        }

        [Test]
        public void GivenUser_WhenTransferMoreMoneyThanHeHasOnHisAccount_ThenInvalidOperationException()
        {
            // Arrange
            var david = _userService.CreateUser("David", "123456");
            var zoran = _userService.CreateUser("Zoran", "1231245");
            _walletService.AddMoney(david.Username, 100);

            // Act
            Assert.Throws<InvalidOperationException>(() => _walletService.Transfer(david.Username, zoran.Username, 200));
            var transactionLogs = _walletService.GetTransactionLogs().ToList();

            // Assert
            Assert.AreEqual(1, transactionLogs.Count());
            var transactionLog = transactionLogs.FirstOrDefault();

            Assert.AreEqual(TransactionStatus.Unsuccessful, transactionLog.TransactionStatus);
        }

        [Test]
        public void GivenUser_WhenTransferMoneyToTheOtherUser_ThenAmountOfMoneyInWalletsOfBothUsersAsExpected()
        {
            // Arrange
            var david = _userService.CreateUser("David", "123456");
            var zoran = _userService.CreateUser("Zoran", "1231245");
            _walletService.AddMoney(david.Username, 100);
            _walletService.AddMoney(zoran.Username, 200);

            // Act
            _walletService.Transfer(david.Username, zoran.Username, 50);
            var transactionLogs = _walletService.GetTransactionLogs().ToList();
            var davidsWallet = _walletService.GetMoney(david.Username);
            var zoransWallet = _walletService.GetMoney(zoran.Username);

            // Assert
            Assert.AreEqual(1, transactionLogs.Count);
            var transactionLog = transactionLogs.FirstOrDefault();

            Assert.AreEqual(50, davidsWallet);
            Assert.AreEqual(250, zoransWallet);
            Assert.AreEqual(TransactionStatus.Successful, transactionLog.TransactionStatus);
        }

        [Test]
        public void GivenUser_WhenTransferMoneyToTheOtherUser_ThenTransactionSuccessful()
        {
            // Arrange
            var david = _userService.CreateUser("David", "123456");
            var zoran = _userService.CreateUser("Zoran", "1231245");
            _walletService.AddMoney(david.Username, 100);
            _walletService.AddMoney(zoran.Username, 200);

            // Act
            _walletService.Transfer(david.Username, zoran.Username, 50);
            var transactionLogs = _walletService.GetTransactionLogs().ToList();

            // Assert
            Assert.AreEqual(1, transactionLogs.Count);
            var transactionLog = transactionLogs.FirstOrDefault();

            Assert.AreEqual(TransactionStatus.Successful, transactionLog.TransactionStatus);
            Assert.AreEqual(david.Username, transactionLog.FromUser);
            Assert.AreEqual(zoran.Username, transactionLog.ToUser);
            Assert.AreEqual(50, transactionLog.Amount);
        }

        [Test]
        public void GivenUser_WhenTransferMoreMoneyThanHeHasOnHisAccount_ThenTransactionUnsuccessful()
        {
            // Arrange
            var david = _userService.CreateUser("David", "123456");
            var zoran = _userService.CreateUser("Zoran", "1231245");
            _walletService.AddMoney(david.Username, 100);
            _walletService.AddMoney(zoran.Username, 200);

            // Act
            Assert.Throws<InvalidOperationException>(() =>
                _walletService.Transfer(david.Username, zoran.Username, 150));
            var transactionLogs = _walletService.GetTransactionLogs().ToList();

            // Assert
            Assert.AreEqual(1, transactionLogs.Count);
            var transactionLog = transactionLogs.FirstOrDefault();

            Assert.AreEqual(TransactionStatus.Unsuccessful, transactionLog.TransactionStatus);
            Assert.AreEqual(david.Username, transactionLog.FromUser);
            Assert.AreEqual(zoran.Username, transactionLog.ToUser);
            Assert.AreEqual(150, transactionLog.Amount);
        }
    }
}