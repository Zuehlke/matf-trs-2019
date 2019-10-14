using System.ComponentModel.DataAnnotations.Schema;

namespace eBidder.Domain
{
    public class AuctionRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionRecordId { get; set; }

        public User User { get; set; }

        public string Reference { get; set; }

        public string Action { get; set; }

        public string Details { get; set; }
    }
}