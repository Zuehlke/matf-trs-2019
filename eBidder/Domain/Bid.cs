using System.ComponentModel.DataAnnotations.Schema;

namespace eBidder.Domain
{
    public class Bid
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BidId { get; set; }

        public User Bidder { get; set; }

        public float Amount { get; set; }

        public float MaxAmount { get; set; }
    }
}