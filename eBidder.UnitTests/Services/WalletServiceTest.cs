using eBidder.Services;
using eBidder.UnitTests.Mocks;
using NUnit.Framework;

namespace eBidder.UnitTests.Services
{
    public class WalletServiceTest
    {
        // TODO (ispit): Rename the UPPERCASE part of the test methods to fit Gherkin convention

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
            Assert.Fail();
        }

        [Test]
        public void GivenNullUsername_WhenAddMoney_DESCRIBE_DESIRED_POSITIVE_OUTCOME()
        {
            Assert.Fail();
        }

        [Test]
        public void ADD_MONEY_FOR_NON_EXISTING_USER()
        {
            Assert.Fail();
        }

        [Test]
        public void ADD_NEGATIVE_AMOUNT_FOR_MONEY()
        {
            Assert.Fail();
        }

        [Test]
        public void GivenUserWithSufficientMoney_WhenRemoveMoney_ThenMoneyDecreased()
        {
            Assert.Fail();
        }
    }
}