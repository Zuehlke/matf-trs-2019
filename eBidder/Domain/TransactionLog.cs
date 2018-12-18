using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eBidder.Domain
{
    public class TransactionLog
    {
        public string FromUser { get; set; }

        public string ToUser { get; set; }

        public double Amount { get; set; }

        public TransactionStatus TransactionStatus { get; set; }
    }
}