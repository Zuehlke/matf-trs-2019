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

    }
}