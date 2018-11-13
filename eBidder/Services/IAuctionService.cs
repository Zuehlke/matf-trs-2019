using System.Collections.Generic;
using eBidder.Models;

namespace eBidder.Services
{
    public interface IAuctionService
    {
        AuctionViewModel CreateAuction(string username, AuctionItemViewModel auctionItemViewModel, int auctionState = 1);

        AuctionViewModel CreateAuction(AuctionViewModel auctionViewModel);

        AuctionViewModel PlaceBid(string username, AuctionViewModel auctionViewModel);

        AuctionViewModel CloseAuction(AuctionViewModel auctionViewModel);

        IEnumerable<AuctionViewModel> GetAllAuctions();

        IEnumerable<AuctionViewModel> GetOpenAuctions();

        IEnumerable<AuctionViewModel> GetAuctionsByUsername(string username);

        IEnumerable<AuctionViewModel> GetAuctionsWithUsersBid(string username);
    }
}