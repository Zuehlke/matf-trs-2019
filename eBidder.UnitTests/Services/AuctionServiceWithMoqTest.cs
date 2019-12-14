using eBidder.Models;
using eBidder.Repositories;
using eBidder.Services;
using eBidder.UnitTests.Builders;
using eBidder.UnitTests.Mocks;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace eBidder.UnitTests.Services
{
    [Ignore("Not executed on exam!")]
    [TestFixture(Category = "AuctionService tests with Moq")]
    public class AuctionServiceWithMoqTest
    {
        // Reuse these fields in tests - hint: think how to initialize these
        IUserService _userService;
        IAuctionService _auctionService;

        string username = "stefan";
        string password = "pass";

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
            // Act
            var auctions = _auctionService.GetAllAuctions();

            // Assert
            Assert.IsEmpty(auctions);
        }

        [Test]
        public void GivenTwoAuctionsWhenGetAllAuctionsThenBothAuctionsReturned()
        {
            // Arrange
            _userService.CreateUser(username, password);
            var auctionItem1 = new AuctionItemBuilder()
                                    .WithMinAmount("100")
                                    .Build();
                
            //new AuctionItemViewModel
            //{
            //    Description = "Description",
            //    MinAmount = "100",
            //    Name = "Auction item"
            //};
            var auctionItem2 =  new AuctionItemViewModel {
                Description = "Description 2",
                MinAmount = "100",
                Name = "Auction item 2"
            };

            _auctionService.CreateAuction(username, auctionItem1);
            _auctionService.CreateAuction(username, auctionItem2);


            // Act
            var auctions = _auctionService.GetAllAuctions();

            // Assert
            Assert.IsNotEmpty(auctions);
            Assert.AreEqual(2, auctions.Count());
        }

        [Test]
        public void GivenNewAuctionWhenGetsByValidUsernameThenNewAuctionIsReturned()
        {
            // Arrange
            _userService.CreateUser(username, password);
            var auctionItem1 = new AuctionItemViewModel
            {
                Description = "Description",
                MinAmount = "100",
                Name = "Auction item"
            };

            // Act
            var newAUction = _auctionService.CreateAuction(username, auctionItem1);
            var retrievedAuctions = _auctionService.GetAuctionsByUsername(username);

            // Assert
            Assert.IsNotEmpty(retrievedAuctions);
            Assert.AreEqual(newAUction.AuctionId, retrievedAuctions.FirstOrDefault().AuctionId);
            Assert.AreEqual(newAUction.Seller, retrievedAuctions.FirstOrDefault().Seller);
        }

        [Test]
        public void GivenOneAuctionWhenGetByInvalidUsernameThenEmptyListReturned()
        {
            // Arrange
            _userService.CreateUser(username, password);
            var auctionItem1 = new AuctionItemViewModel
            {
                Description = "Description",
                MinAmount = "100",
                Name = "Auction item"
            }; ;

            // Act
            var newAUction = _auctionService.CreateAuction(username, auctionItem1);
            var retrievedAuctions = _auctionService.GetAuctionsByUsername("another username");

            // Assert
            Assert.IsEmpty(retrievedAuctions);
        }

        [Test]
        public void GivenOneOpenAndTwoClosedAuctionsWhenGetOpenAuctionsThenOneAuctionIsReturned()
        {
            // Arrange
            _userService.CreateUser(username, password);

            var auctionItem1 = new AuctionItemViewModel {
                Description = "Description",
                MinAmount = "100",
                Name = "Auction item"
            };

            // Act
            var openedAuction = _auctionService.CreateAuction(username, auctionItem1);
            var closedAuction = _auctionService.CreateAuction(username, auctionItem1, 2);
            var closedAuction1 = _auctionService.CreateAuction(username, auctionItem1, 2);

            var retrievedAuctions = _auctionService.GetOpenAuctions();

            // Assert
            Assert.IsNotEmpty(retrievedAuctions);
            Assert.AreEqual(1, retrievedAuctions.Count());
            Assert.AreEqual(openedAuction.AuctionId, retrievedAuctions.FirstOrDefault().AuctionId);
        }

        [Test]
        public void GivenTwoPendingAuctionsWhenGetOpenAuctionsThenEmptyListReturned()
        {
            // Arrange
            _userService.CreateUser(username, password);
            var auctionItem1 = new AuctionItemViewModel
            {
                Description = "Description",
                MinAmount = "100",
                Name = "Auction item"
            }; ;

            // Act
            var pendingAuction = _auctionService.CreateAuction(username, auctionItem1, 0);
            var pendingAuction1 = _auctionService.CreateAuction(username, auctionItem1, 0);

            var retrievedAuctions = _auctionService.GetOpenAuctions();

            // Assert
            Assert.IsEmpty(retrievedAuctions);
        }

        [Test]
        public void GivenOneOpenAuctionWhenCloseAuctionThenAuctionIsClosed()
        {
            // Arrange
            _userService.CreateUser(username, password);
            var auctionItem = new AuctionItemViewModel
            {
                Description = "Description",
                MinAmount = "100",
                Name = "Auction item"
            };

            // Act
            var newAuction = _auctionService.CreateAuction(username, auctionItem);
            var closedAuction = _auctionService.CloseAuction(newAuction);

            // Assert
            Assert.AreEqual(newAuction.AuctionId, closedAuction.AuctionId);
            Assert.AreEqual(2, closedAuction.AuctionState);
        }

        [Test]
        public void GivenAuctionWhenPlaceBidThenNewBidIsSaved()
        {
            // Arrange
            _userService.CreateUser("anotherUser", password);
            UserSession.CurrentUser = _userService.CreateUser(username, password);
            var auction = new AuctionBuilder()
                                .WithSeller("anotherUser")
                                .WithMinAmount("100")
                                .Build();

            var newAuction = _auctionService.CreateAuction(auction);
            newAuction.BidAmount = "150";

            // Act
            var biddedAuction = _auctionService.PlaceBid(username, newAuction);

            // Assert
            Assert.IsNotEmpty(biddedAuction.Bids);
            Assert.AreEqual(150L, biddedAuction.Bids.FirstOrDefault());
        }

        [Test]
        public void GivenAuctionWithSameSellerWhenPlaceBidThenExceptionIsThrown()
        {
            // Arrange
            UserSession.CurrentUser = _userService.CreateUser(username, password);
            var auction = new AuctionBuilder()
                                .WithSeller(username)
                                .WithMinAmount("100")
                                .Build();

            var newAuction = _auctionService.CreateAuction(auction);
            newAuction.BidAmount = "150";

            // Act
            void PlaceBid()
            {
                _auctionService.PlaceBid(username, newAuction);
            }

            // Assert
            Assert.That(() => PlaceBid(), Throws.InvalidOperationException);
        }

        [Test]
        public void GivenAuctionWithSmallerBidAmountThenExceptionIsThrown()
        {
            // Arrange
            _userService.CreateUser("anotherUser", password);
            UserSession.CurrentUser = _userService.CreateUser(username, password);
            var auction = new AuctionBuilder()
                                .WithSeller("anotherUser")
                                .WithMinAmount("100")
                                .Build();

            var newAuction = _auctionService.CreateAuction(auction);
            newAuction.BidAmount = "50";

            // Act
            void PlaceBid()
            {
                _auctionService.PlaceBid(username, newAuction);
            }

            // Assert
            Assert.That(() => PlaceBid(), Throws.ArgumentException);
        }

        [Test]
        public void GivenAuctionWithNoBidsWhenGetByUserBidsThenEmptyListReturned()
        {
            // Arrange
            _userService.CreateUser("anotherUser", password);
            UserSession.CurrentUser = _userService.CreateUser(username, password);

            var auction = new AuctionBuilder()
                                .WithSeller("anotherUser")
                                .WithMinAmount("100")
                                .Build();
            var newAuction = _auctionService.CreateAuction(auction);

            // Act
            var biddedAuctions = _auctionService.GetAuctionsWithUsersBid(username);

            // Assert
            Assert.IsEmpty(biddedAuctions);
        }

        [Test]
        public void GetAuctionsWithUsersBidPositiveTest()
        {
            // Arrange
            _userService.CreateUser("anotherUser", password);
            UserSession.CurrentUser = _userService.CreateUser(username, password);

            var auction = new AuctionBuilder()
                                .WithSeller("anotherUser")
                                .WithMinAmount("100")
                                .Build();
            var newAuction = _auctionService.CreateAuction(auction);
            newAuction.BidAmount = "150";
            _auctionService.PlaceBid(username, newAuction);

            // Act
            var biddedAuctions = _auctionService.GetAuctionsWithUsersBid(username);

            // Assert
            Assert.IsNotEmpty(biddedAuctions);
            Assert.AreEqual(1, biddedAuctions.Count());
            Assert.AreEqual(150F, biddedAuctions.FirstOrDefault()?.Bids.FirstOrDefault());
        }
    }
}
