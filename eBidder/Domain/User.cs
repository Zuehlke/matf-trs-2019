using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBidder.Domain
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Auction> Auctions { get; set; }

    }
}