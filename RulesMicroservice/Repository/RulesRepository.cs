using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RulesMicroservice.Repository
{
    public class RulesRepository : IRulesRepository
    {
        Uri baseAddress = new Uri("https://localhost:44379/api");   //Port No.
        HttpClient client;
        public RulesRepository()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public List<rulesMessage> evaluateMinBalCurrent()
        {
            List<currentAccountDetails> ls = new List<currentAccountDetails>();
            var msg = new List<rulesMessage>();
            var a = new rulesMessage();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Account/getCurrentAccountList").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<currentAccountDetails>>(data);
            }
            foreach (var v in ls)
            {
                if (v.currentAccountBalance < 500)
                {
                    a.accountId = v.currentAccountId;
                    a.accountBalance = v.currentAccountBalance;
                    a.message = "Service Charge Applicable";
                    msg.Add(a);
                }
            }
            return msg;
        }
        public List<rulesMessage> evaluateMinBalSavings()
        {
            List<SavingsAccount> ls1 = new List<SavingsAccount>();
            var msg = new List<rulesMessage>();
            var a = new rulesMessage();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Account/getSavingAccountList").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ls1 = JsonConvert.DeserializeObject<List<SavingsAccount>>(data);
            }
            foreach (var v in ls1)
            {
                if (v.savingsAccountbalance < 500)
                {
                    //v.SBal = v.SBal - 100;
                    a.accountId = v.savingsAccountId;
                    a.accountBalance = v.savingsAccountbalance;
                    //v.SBal = v.SBal - 100;
                    a.message = "Service Charge Applicable";
                    msg.Add(a);
                }
            }
            return msg;
        }
        public ServiceCharge getServiceCharges()
        {
            ServiceCharge ob = new ServiceCharge();
            ob.serviceChargeApplicable = 100F;
            return ob;
        }
        public ruleStatus evaluateMinBal(transactionInput value)
        {
            accountDetails ob = new accountDetails();
            ruleStatus ob1 = new ruleStatus();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Account/getAccount/" + value.accountId).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ob = JsonConvert.DeserializeObject<accountDetails>(data);
                ob.accountBalance = ob.accountBalance - value.balance;
                if (ob.accountBalance < 500)
                    ob1.message = "Warning";
                else
                    ob1.message = "No Warning";
            }
            return ob1;
        }
    }
}
