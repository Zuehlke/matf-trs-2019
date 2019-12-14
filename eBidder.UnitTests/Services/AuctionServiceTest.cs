using System;
using System.Linq;
using eBidder.Models;
using eBidder.Repositories;
using eBidder.Services;
using eBidder.UnitTests.Fakes;
using NUnit.Framework;

namespace eBidder.UnitTests.Services
{
    [Ignore("Not executed on exam!")]
    class AuctionServiceTest
    {
        [Test]
        public void GivenEmptyAuctionRepository_WhenGetAllAuctionsIsCalled_EmptyListIsReturned()
        {
            // Arrange
            var auctionService = new AuctionService(new AuctionRepositoryFake(), new UserRepositoryFake(), new AuditRepositoryFake());

            // Act
            var allAuctions = auctionService.GetAllAuctions();

            // Assert
            Assert.IsEmpty(allAuctions);
        }

        [Test]
        public void GivenRepositoriesWithOneAuctionAndOneUser_WhenGetAllAuctionsIsCalled_ListContainingThatAuctionIsReturned()
        {
            // Arrange
            const string username = "Pera";
            var userFake = new UserRepositoryFake();

            userFake.CreateUser(username, "123456");
            var auctionService = new AuctionService(new AuctionRepositoryFake(), userFake, new AuditRepositoryFake());

            // Act
            auctionService.CreateAuction(username, new AuctionItemViewModel {Description = "description", MinAmount = "0", Name = "Item"});

            // Assert
            var allAuctions = auctionService.GetAllAuctions();
            var auctionViewModels = allAuctions.ToList();
            var firstAuction = auctionViewModels.FirstOrDefault();

            Assert.IsNotEmpty(auctionViewModels);
            Assert.AreEqual(username, firstAuction.Seller);
        }

        [Test]
        public void GivenNullAsOneOfTheArguments_WhenCreateAuctionIsCalled_ExceptionIsThrown()
        {
            // Arrange
            var auctionService = new AuctionService(new AuctionRepositoryFake(), new UserRepositoryFake(), new AuditRepositoryFake());

            // Act & Assert
            Assert.Throws<ArgumentException>(() => auctionService.CreateAuction(null, new AuctionItemViewModel { Description = "description", MinAmount = "0", Name = "Item" }));
            Assert.Throws<ArgumentNullException>(() => auctionService.CreateAuction("Pera", null));
        }

        [Test]
        public void GivenTwoCreatedAuctionsByASingleUser_GetAuctionsByUsername_ReturnsListContainingBothAuctions()
        {
            // Arrange
            const string username = "Pera";
            var userFake = new UserRepositoryFake();

            userFake.CreateUser(username, "123456");
            var auctionService = new AuctionService(new AuctionRepositoryFake(), userFake, new AuditRepositoryFake());
            auctionService.CreateAuction(username, new AuctionItemViewModel { Description = "description", MinAmount = "0", Name = "Item 1" });
            auctionService.CreateAuction(username, new AuctionItemViewModel { Description = "description 2", MinAmount = "0", Name = "Item 2" });

            // Act
            var allAuctionsByUsername = auctionService.GetAuctionsByUsername(username);

            // Assert
            var auctionViewModels = allAuctionsByUsername.ToList();
            var firstAuction = auctionViewModels.FirstOrDefault();
            var lastAuction = auctionViewModels.LastOrDefault();

            Assert.AreEqual(2, auctionViewModels.Count);
            Assert.AreEqual(username, firstAuction.Seller);
            Assert.AreEqual(username, lastAuction.Seller);
        }

        [Test]
        public void GivenAuctionsByASingleUser_GetAuctionsByUsername_WithDifferentUsername_ReturnsEmptyList()
        {
            // Arrange
            const string pera = "Pera";
            const string mika = "Mika";
            var userFake = new UserRepositoryFake();

            userFake.CreateUser(pera, "123456");
            userFake.CreateUser(mika, "654321");

            var auctionService = new AuctionService(new AuctionRepositoryFake(), userFake, new AuditRepositoryFake());
            auctionService.CreateAuction(pera, new AuctionItemViewModel { Description = "description", MinAmount = "0", Name = "Item 1" });
            auctionService.CreateAuction(pera, new AuctionItemViewModel { Description = "description 2", MinAmount = "0", Name = "Item 2" });

            // Act
            var allAuctionsByUsername = auctionService.GetAuctionsByUsername(mika);

            // Assert
            Assert.IsEmpty(allAuctionsByUsername);
        }
    }
}