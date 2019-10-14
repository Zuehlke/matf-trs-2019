using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBidder.Domain
{
    public class Auction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionId { get; set; }

        public User Seller { get; set; }

        public AuctionItem AuctionItem { get; set; }

        public AuctionState AuctionState { get; set; }

        public Bid CurrentBid { get; set; }

        // Nav property
        public virtual ICollection<Bid> Bids { get; set; }

        public float MinAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}