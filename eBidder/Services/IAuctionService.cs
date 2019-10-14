using System.Collections.Generic;
using eBidder.Models;

namespace eBidder.Services
{
    public interface IAuctionService
    {
        AuctionViewModel CreateAuction(string username, AuctionItemViewModel auctionViewModel, int auctionState = 1);

        bool PlaceBid(string username, AuctionViewModel auctionViewModel);

        void CloseAuction(AuctionViewModel auctionViewModel);

        IEnumerable<AuctionViewModel> GetAllAuctions();

        IEnumerable<AuctionViewModel> GetOpenAuctions();

        IEnumerable<AuctionViewModel> GetAuctionsByUsername(string username);

        IEnumerable<AuctionViewModel> GetAuctionsWithUsersBid(string username);
    }
}