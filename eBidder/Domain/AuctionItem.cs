using System.ComponentModel.DataAnnotations.Schema;

namespace eBidder.Domain
{
    public class AuctionItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionItemId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}