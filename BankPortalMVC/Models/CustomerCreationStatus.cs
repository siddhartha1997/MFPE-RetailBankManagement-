using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankPortalMVC.Models
{
    public class CustomerCreationStatus
    {
        public string Message { get; set; }
        public int customerId { get; set; }
        public int currentAccountId { get; set; }
        public int savingsAccountId { get; set; }
    }
}
