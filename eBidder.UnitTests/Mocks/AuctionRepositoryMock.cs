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
            throw new NotImplementedException();
        }
    }
}