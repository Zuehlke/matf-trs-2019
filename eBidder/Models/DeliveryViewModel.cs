using System;

namespace eBidder.Models
{
    public class DeliveryViewModel
    {
        public int DeliveryId { get; set; }

        public string Recepient { get; set; }
        
        public string DeliveryItem { get; set; }

        public DateTime? DeliveredDate { get; set; }
        
        public DateTime? SentDate { get; set; }
        
        public int Status { get; set; }
    }
}