using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankPortalMVC.Models
{
    public class ViewAccountCustRes
    {
        [Key]
        public int id { get; set; }
        public int CustId { get; set; }
        public int AccId { get; set; }
        public string AccType { get; set; }
        public double AccBal { get; set; }
    }
}
