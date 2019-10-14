using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eBidder.Models
{
    public class AuctionViewModel
    {
        [Display(Name = "Seller")]
        public string Seller { get; set; }

        public AuctionItemViewModel AuctionItem { get; set; }

        public int AuctionState { get; set; }

        [Display(Name = "Your current bid")]
        public string UserAmount { get; set; }

        [Display(Name = "Enter your offer here")]
        public string BidAmount { get; set; }

        public virtual ICollection<float> Bids { get; set; }

        [Display(Name = "Minimum amount")]
        public string MinAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}