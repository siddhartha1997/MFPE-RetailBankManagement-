using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankPortalMVC.Models
{
    public class transfers
    {
        public int senderAccountId { get; set; }
        public int receiverAccountId { get; set; }
        public int amount { get; set; }
    }
}
