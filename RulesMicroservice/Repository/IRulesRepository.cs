using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RulesMicroservice.Repository
{
    public interface IRulesRepository
    {
        public List<rulesMessage> evaluateMinBalCurrent();
        public List<rulesMessage> evaluateMinBalSavings();
        public ServiceCharge getServiceCharges();
        
        public ruleStatus evaluateMinBal(transactionInput value);



    }
}
