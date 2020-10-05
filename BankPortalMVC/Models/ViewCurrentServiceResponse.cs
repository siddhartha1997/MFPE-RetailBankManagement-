using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankPortalMVC.Models
{
    public class ViewCurrentServiceResponse
    {
        [Key]
        public int id { get; set; }
        public int AccId { get; set; }
        public double AccBal { get; set; }
        public string Message { get; set; }
    }
}
