using eBidder.Domain;
using eBidder.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eBidder.UnitTests.Mocks
{
    /// <summary>
    /// This is one way of doing things, we could mock the things we need directly in test class/test method
    /// </summary>
    internal class AuctionRepositoryMock
    {
        ICollection<Auction> _auctions;

        internal IAuctionRepository CreateRepository()
        {
            _auctions = new List<Auction>();

            var fakeRepository = new Mock<IAuctionRepository>();

            fakeRepository.Setup(x => x.GetAuctions()).Returns(() => _auctions);

            fakeRepository.Setup(x => x.PlaceBid(It.IsAny<User>(), It.IsAny<Auction>(), It.IsAny<float>()))
                          .Callback((User user, Auction auction, float amount) =>
                                _auctions.FirstOrDefault((a => a.AuctionId == auction.AuctionId)).Bids = new List<Bid> { new Bid { Amount = amount, Bidder = user } })
                          .Returns((User user, Auction auction, float amount) =>
                                _auctions.FirstOrDefault((a => a.AuctionId == auction.AuctionId)));

            fakeRepository.Setup(x => x.GetAuctionsWithUsersBid(It.IsAny<string>()))
                          .Returns((string username) => _auctions.Where(a => a.Bids.Any(b => b.Bidder.Username.Equals(username, StringComparison.OrdinalIgnoreCase))));

            fakeRepository.Setup(x => x.GetAuctionByUsername(It.IsAny<string>()))
                        .Returns((string username) => _auctions.Where(a => a.Seller.Username.Equals(username, StringComparison.OrdinalIgnoreCase)));

            fakeRepository.Setup(x => x.CreateAuction(It.IsAny<Auction>()))
                           .Callback((Auction auction) => _auctions.Add(auction))
                           .Returns((Auction auction) => _auctions.FirstOrDefault(a => a.AuctionId == auction.AuctionId));

            fakeRepository.Setup(x => x.CloseAuction(It.IsAny<Auction>()))
                          .Callback((Auction auction) => (_auctions.FirstOrDefault(a => a.AuctionId == auction.AuctionId)).AuctionState = AuctionState.Closed)
                          .Returns((Auction auction) => _auctions.FirstOrDefault(a => a.AuctionId == auction.AuctionId));

            return fakeRepository.Object;
        }
    }
}