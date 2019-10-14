using System.ComponentModel.DataAnnotations;

namespace eBidder.Models
{
    public class AuctionItemViewModel
    {
        [Required]
        [Display(Name = "Item name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Item description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Minimum amount")]
        public string MinAmount { get; set; }
    }
}