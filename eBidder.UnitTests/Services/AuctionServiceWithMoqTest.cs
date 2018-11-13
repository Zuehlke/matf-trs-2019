using eBidder.Repositories;
using eBidder.Services;
using eBidder.UnitTests.Mocks;
using Moq;
using NUnit.Framework;

namespace eBidder.UnitTests.Services
{
    [TestFixture(Category = "AuctionService tests with Moq")]
    public class AuctionServiceWithMoqTest
    {
        // Reuse these fields in tests - hint: think how to initialize these
        IUserService _userService;
        IAuctionService _auctionService;

        private void InitializeServices()
        {
            // Init UserService
            var fakeUserRepository = new UserRepositoryMock().CreateRepository();
            _userService = new UserService(fakeUserRepository);

            var fakeAuctionRepository = new AuctionRepositoryMock().CreateRepository();
            var fakeAuditRepository = new Mock<IAuditRepository>().Object;

            _auctionService = new AuctionService(fakeAuctionRepository, fakeUserRepository, fakeAuditRepository);
        }

        // Use SetUp to init all services that we need by calling method above
        [SetUp]
        public void Init()
        {
            InitializeServices();
        }

        [Test]
        public void GivenNoAuctionsWhenGetAllAuctionsThenEmptyListReturned()
        {
            Assert.Fail();
        }

        [Test]
        public void GivenTwoAuctionsWhenGetAllAuctionsThenBothAuctionsReturned()
        {
            Assert.Fail();
        }

        [Test]
        public void GivenNewAuctionWhenGetsByValidUsernameThenNewAuctionIsReturned()
        {
            Assert.Fail();
        }

        [Test]
        public void GivenOneAuctionWhenGetByInvalidUsernameThenEmptyListReturned()
        {
            Assert.Fail();
        }

        [Test]
        public void GivenOneOpenAndTwoClosedAuctionsWhenGetOpenAuctionsThenOneAuctionIsReturned()
        {
            Assert.Fail();
        }

        [Test]
        public void GivenTwoPendingAuctionsWhenGetOpenAuctionsThenEmptyListReturned()
        {
            Assert.Fail();
        }

        [Test]
        public void GivenOneOpenAuctionWhenCloseAuctionThenAuctionIsClosed()
        {
            Assert.Fail();
        }
    }
}
