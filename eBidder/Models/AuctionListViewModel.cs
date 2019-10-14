using System.Collections.Generic;

namespace eBidder.Models
{
    public class AuctionListViewModel
    {
        public List<AuctionViewModel> ViewModels { get; set; }

        public AuctionListViewModel()
        {
            ViewModels = new List<AuctionViewModel>();
        }

        public AuctionListViewModel(List<AuctionViewModel> auctions)
        {
            ViewModels = auctions;
        }
    }
}