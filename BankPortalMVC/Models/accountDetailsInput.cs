using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankPortalMVC.Models
{
    public class accountDetailsInput
    {
        public int accountId { get; set; }
        public int fromDate { get; set; }
        public int toDate { get; set; }
    }
}
