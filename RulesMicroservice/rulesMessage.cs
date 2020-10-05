using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RulesMicroservice
{
    public class rulesMessage
    {
        public int accountId { get; set; }
        public double accountBalance { get; set; }
        public string message { get; set; }
    }
}
