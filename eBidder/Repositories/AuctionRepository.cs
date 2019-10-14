using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using eBidder.Domain;

namespace eBidder.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly ApplicationDbContext _context;

        public AuctionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Auction> GetAuctions()
        {
            return _context.Auctions
                .Include(x => x.Seller)
                .Include(x => x.AuctionItem)
                .Include(x => x.Bids.Select(b => b.Bidder))
                .ToList();
        }

        public IEnumerable<Auction> GetAuctionByUsername(string username)
        {
            return _context.Auctions
                .Include(x => x.Seller)
                .Include(x => x.AuctionItem)
                .Include(x => x.Bids.Select(b => b.Bidder))
                .Where(a => a.Seller.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public Auction CreateAuction(Auction auction)
        {
            var newAuction = _context.Auctions.Add(auction);
            _context.SaveChanges();

            return newAuction;
        }

        public bool PlaceBid(User user, Auction auction, float amount)
        {
            var auctionDb = GetAuctionFromDb(auction);
            var newBid = SetNewBidForAuction(user, amount, auctionDb);

            _context.Bids.Add(newBid);
            _context.SaveChanges();

            return true;
        }

        public IEnumerable<Auction> GetAuctionsWithUsersBid(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            var auctionsWithUserBids = _context.Auctions
                .Include(x => x.Seller)
                .Include(x => x.AuctionItem)
                .Include(x => x.Bids.Select(b => b.Bidder))
                .Where(a => a.Bids.Any(b => b.Bidder.UserId.Equals(user.UserId)));

            return auctionsWithUserBids.ToList();
        }


        public void CloseAuction(Auction auction)
        {
            var auctionFromDb = GetAuctionFromDb(auction);
            auctionFromDb.AuctionState = AuctionState.Closed;
            _context.SaveChanges();
        }

        private static Expression<Func<Auction, bool>> HasSameSellerAndItem(Auction auction)
        {
            return a => a.AuctionItem.Name.Equals(auction.AuctionItem.Name, StringComparison.OrdinalIgnoreCase) &&
                        a.Seller.Username.Equals(auction.Seller.Username, StringComparison.OrdinalIgnoreCase);
        }

        private static Bid SetNewBidForAuction(User user, float amount, Auction auctionDb)
        {
            var newBid = new Bid
            {
                Bidder = user,
                Amount = amount
            };

            if (auctionDb.Bids == null)
            {
                auctionDb.Bids = new List<Bid> { newBid };
            }
            else
            {
                auctionDb.Bids.Add(newBid);
            }

            auctionDb.CurrentBid = auctionDb.Bids.OrderByDescending(b => b.Amount).FirstOrDefault();
            return newBid;
        }

        private Auction GetAuctionFromDb(Auction auction)
        {
            var auctionDb = _context.Auctions
                                .Include(x => x.Seller)
                                .Include(x => x.AuctionItem)
                                .Where(HasSameSellerAndItem(auction)).FirstOrDefault() ?? auction;
            return auctionDb;
        }
    }
}