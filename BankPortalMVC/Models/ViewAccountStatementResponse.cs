﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankPortalMVC.Models
{
    public class ViewAccountStatementResponse
    {
        [Key]
        public int id { get; set; }
        public int date { get; set; }
        public string Narration { get; set; }
        public int refno { get; set; }
        public int valueDate { get; set; }
        public double withdrawal { get; set; }
        public double deposit { get; set; }
        public double closingBalance { get; set; }
    }
}
