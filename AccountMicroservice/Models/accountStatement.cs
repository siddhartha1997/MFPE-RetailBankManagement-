using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroservice.Models
{
    public class accountStatement
    {
        public int accountId { get; set; }
        public List<statement> statements { get; set; }
    }
}
