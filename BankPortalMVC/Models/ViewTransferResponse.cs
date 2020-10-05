using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankPortalMVC.Models
{
    public class ViewTransferResponse
    {
        [Key]
        public int id { get; set; }
        public double sbal { get; set; }
        public double rbal { get; set; }
        public string transferStatus { get; set; }
    }
}
