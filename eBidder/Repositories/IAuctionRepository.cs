using System.Collections.Generic;
using eBidder.Domain;

namespace eBidder.Repositories
{
    public interface IAuctionRepository
    {
        IEnumerable<Auction> GetAuctions();

        IEnumerable<Auction> GetAuctionByUsername(string username);

        Auction CreateAuction(Auction auction);

        Auction PlaceBid(User username, Auction auction, float amount);

        IEnumerable<Auction> GetAuctionsWithUsersBid(string username);

        Auction CloseAuction(Auction auction);
    }
}