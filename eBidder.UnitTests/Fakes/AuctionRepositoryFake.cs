using System;
using System.Collections.Generic;
using System.Linq;
using eBidder.Domain;
using eBidder.Repositories;

namespace eBidder.UnitTests.Fakes
{
    public class AuctionRepositoryFake : IAuctionRepository
    {
        private readonly List<Auction> _auctions = new List<Auction>();

        public void CloseAuction(Auction auction)
        {
            var closingAuction = _auctions.Find(x => x == auction);
            closingAuction.AuctionState = AuctionState.Closed;
        }

        public Auction CreateAuction(Auction auction)
        {
            _auctions.Add(auction);
            return auction;
        }

        public IEnumerable<Auction> GetAuctionByUsername(string username)
        {
            return _auctions.Where(a => a.Seller.Username == username);
        }

        public IEnumerable<Auction> GetAuctions()
        {
            return _auctions;
        }

        public IEnumerable<Auction> GetAuctionsWithUsersBid(string username)
        {
            return _auctions.Where(x => x.Bids.Any(b => b.Bidder.Username == username));
        }

        public bool PlaceBid(User username, Auction auction, float amount)
        {
            try 
            {
                var biddedAuction = _auctions.First(x => x == auction);
                biddedAuction.Bids.Add(new Bid { Amount = amount, Bidder = username });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}