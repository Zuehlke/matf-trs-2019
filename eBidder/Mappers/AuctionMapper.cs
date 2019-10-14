using System;
using System.Globalization;
using System.Linq;
using eBidder.Domain;
using eBidder.Models;

namespace eBidder.Mappers
{
    public static class AuctionMapper
    {
        public static Auction ToAuction(this AuctionViewModel viewModel, User user)
        {
            return new Auction
            {
                AuctionItem = viewModel.AuctionItem.ToAuctionItem(),
                MinAmount = float.Parse(viewModel.MinAmount),
                AuctionState = (AuctionState)viewModel.AuctionState,
                EndDate = viewModel.EndDate,
                StartDate = viewModel.StartDate,
                Seller = user,
                Bids = viewModel.Bids?.Select(b => new Bid
                {
                    Amount = b,
                    Bidder = user
                }).ToList()
            };
        }

        public static AuctionViewModel ToAuctionViewModel(this Auction auction)
        {
            return new AuctionViewModel
            {
                AuctionItem = auction.AuctionItem?.ToAuctionItemViewModel(),
                StartDate = auction.StartDate,
                EndDate = auction.EndDate,
                MinAmount = auction.MinAmount.ToString(CultureInfo.InvariantCulture),
                Seller = auction.Seller.Username,
                Bids = auction.Bids?.Select(b => b.Amount).ToList(),
                AuctionState = (int)auction.AuctionState,
                UserAmount = GetUsersBidAmount(auction)
            };
        }

        private static string GetUsersBidAmount(Auction auction)
        {
            if (auction.Bids == null || !auction.Bids.Any())
            {
                return string.Empty;
            }

            var userBids = auction.Bids
                .Where(b => b.Bidder.Username.Equals(UserSession.CurrentUser.Username, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!userBids.Any())
            {
                return string.Empty;
            }

            return userBids.Select(b => b.Amount).Max().ToString(CultureInfo.InvariantCulture);
        }
    }
}