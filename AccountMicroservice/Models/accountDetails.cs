using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroservice.Models
{
    public class accountDetails
    {
        public int accountId { get; set; }
        public string accountType { get; set; }
        public double accountBalance { get; set; }
    }
}
