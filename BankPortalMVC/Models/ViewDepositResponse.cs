using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankPortalMVC.Models
{
    public class ViewDepositResponse
    {
        [Key]
        public int id { get; set; }
        public int AccId { get; set; }
        public string DepositStatus { get; set; }
        public double AccBal { get; set; }
    }
}
