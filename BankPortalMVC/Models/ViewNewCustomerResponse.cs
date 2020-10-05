using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankPortalMVC.Models
{
    public class ViewNewCustomerResponse
    {
        [Key]
        public int id { get; set; }
        public string Message { get; set; }
        public int CustomerId { get; set; }
        public int CurrentAccountId { get; set; }
        public int SavingsAccountId { get; set; }
    }
}
