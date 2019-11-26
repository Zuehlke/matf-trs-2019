using eBidder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBidder.UnitTests.Builders
{
    public class AuctionBuilder
    {
        private int auctionId;
        private string userAmount;
        private DateTime startDate;
        private string seller;
        private string minAmount;
        private DateTime endDate;
        private IList<float> bids;
        private string bidAmount;
        private int auctionState;
        private AuctionItemViewModel auctionItem;

        public AuctionBuilder()
        {
            auctionId = 1;
            userAmount = "123";
            startDate = DateTime.Now;
            seller = "stefan";
            minAmount = "100";
            endDate = DateTime.Now.AddSeconds(1);
            bids = new List<float> { 100, 120, 121, 122, 123 };
            bidAmount = "123";
            auctionState = 1;
            auctionItem = new AuctionItemViewModel
            {
                Description = "some description",
                MinAmount = "123",
                Name = "auction item"
            };
        }

        public AuctionBuilder WithAuctionId(int auctionId)
        {
            this.auctionId = auctionId;
            return this;
        }

        public AuctionBuilder WithUserAmount(string userAmount)
        {
            this.userAmount = userAmount;
            return this;
        }

        public AuctionBuilder WithStartDate(DateTime startDate)
        {
            this.startDate = startDate;
            return this;
        }

        public AuctionBuilder WithSeller(string username)
        {
            seller = username;
            return this;
        }

        public AuctionBuilder WithMinAmount(string minAmount)
        {
            this.minAmount = minAmount;
            return this;
        }

        public AuctionBuilder WithEndDate(DateTime endDate)
        {
            this.endDate = endDate;
            return this;
        }

        public AuctionBuilder WithBids(params float[] bids)
        {
            // Clear existing list
            this.bids.Clear();

            foreach (var bid in bids)
            {
                this.bids.Add(bid);
            }

            return this;
        }

        public AuctionBuilder WithBidAmount(string bidAmount)
        {
            this.bidAmount = bidAmount;
            return this;
        }

        /// <summary>
        /// Auction state values:
        /// 0 - Pending,
        /// 1 - Open,
        /// 2 - Closed
        /// </summary>
        /// <param name="state"></param>
        public AuctionBuilder WithAuctionState(int state)
        {
            auctionState = state;
            return this;
        }

        public AuctionBuilder WithAuctionItem(AuctionItemViewModel auctionItem)
        {
            this.auctionItem = auctionItem;
            return this;
        }

        public AuctionViewModel Build()
        {
            return new AuctionViewModel
            {
                AuctionId = auctionId,
                AuctionItem = auctionItem,
                AuctionState = auctionState,
                BidAmount = bidAmount,
                Bids = bids,
                EndDate = endDate,
                MinAmount = minAmount,
                Seller = seller,
                StartDate = startDate,
                UserAmount = userAmount
            };
        }
    }
}
