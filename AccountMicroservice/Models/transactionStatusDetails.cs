using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroservice.Models
{
    public class transactionStatusDetails
    {
        public double senderBalance { get; set; }
        public double receiverBalance { get; set; }
        public string transferStatus { get; set; }
    }
}
