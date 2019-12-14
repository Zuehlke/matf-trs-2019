using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBidder.Domain
{
    public class Delivery
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeliveryId { get; set; }

        public string Recepient { get; set; }
        
        public string DeliveryItem { get; set; }

        public DateTime? DeliveredDate { get; set; }
        
        public DateTime? SentDate { get; set; }
        
        public DeliveryStatus Status { get; set; }
    }
}