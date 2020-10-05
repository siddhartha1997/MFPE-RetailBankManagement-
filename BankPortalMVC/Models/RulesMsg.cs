using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankPortalMVC.Models
{
    public class RulesMsg
    {
        public int accountId { get; set; }
        public double accountBalance { get; set; }
        public string message { get; set; }
    }
}
