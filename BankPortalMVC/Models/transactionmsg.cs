using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankPortalMVC.Models
{
    public class transactionmsg
    {
        public double senderBalance { get; set; }
        public double receiverBalance { get; set; }
        public string transferStatus { get; set; }
    }
}
