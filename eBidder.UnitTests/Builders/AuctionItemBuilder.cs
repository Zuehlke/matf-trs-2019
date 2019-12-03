using eBidder.Models;

namespace eBidder.UnitTests.Builders
{
    public class AuctionItemBuilder
    {
        private string description;
        private string minAmount;
        private string name;

        public AuctionItemBuilder()
        {
            description = "some description";
            minAmount = "123";
            name = "auction item";
        }

        public AuctionItemBuilder WithDescription(string description)
        {
            this.description = description;
            return this;
        }

        public AuctionItemBuilder WithMinAmount(string minAmount)
        {
            this.minAmount = minAmount;
            return this;
        }

        public AuctionItemBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public AuctionItemViewModel Build()
        {
            return new AuctionItemViewModel
            {
                Description = description,
                MinAmount = minAmount,
                Name = name
            };
        }
    }
}