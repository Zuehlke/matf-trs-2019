using eBidder.Domain;
using eBidder.Models;

namespace eBidder.Mappers
{
    public static class AuctionItemMapper
    {
        public static AuctionItem ToAuctionItem(this AuctionItemViewModel viewModel)
        {
            return new AuctionItem
            {
                Name = viewModel.Name,
                Description = viewModel.Description
            };
        }

        public static AuctionItemViewModel ToAuctionItemViewModel(this AuctionItem auctionItem)
        {
            return new AuctionItemViewModel
            {
                Description = auctionItem.Description,
                Name = auctionItem.Name
            };
        }
    }
}