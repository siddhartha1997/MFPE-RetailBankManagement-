using AccountMicroservice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AccountMicroservice.Repository
{
    public class accountRepository : IAccountRepository
    {
        int acid = 1;
        Uri baseAddress = new Uri("https://localhost:44304/api");   //Port No.
        HttpClient client;
        public accountRepository()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public static List<accountStatement> accountStatements = new List<accountStatement>()
        {
            new accountStatement{accountId=202,
            statements= new List<statement>()
            {
                new statement{date=21092020,narration="Withdrawn",refNumber=12345,valueDate=01022020,withdrawal=1000.00,deposit=0.00,closingBalance=1000.00},
                new statement{date=27092020,narration="Deposited",refNumber=21345,valueDate=04022020,withdrawal=0.00,deposit=2000.00,closingBalance=3000.00}
                }
            }
         };
        public static List<customerAccount> customeraccounts = new List<customerAccount>()
        {
            new customerAccount{customerId=2,currentAccountId=201,savingsAccountId=202}
        };
        public static List<currentAccountDetails> currentAccounts = new List<currentAccountDetails>()
        {
            new currentAccountDetails{currentAccountId=201,currentAccountBalance=1000}
        };
        public static List<savingsAccount> savingsAccounts = new List<savingsAccount>()
        {
            new savingsAccount{savingsAccountId=202,savingsAccountbalance=500}
        };
        public List<currentAccountDetails> getCurrent()
        {
            return currentAccounts;
        }

        public List<savingsAccount> getSavings()
        {
            return savingsAccounts;
        }

        public List<accountDetails> getCustomerAccounts(int id)
        {
            var a = customeraccounts.Where(c => c.customerId == id).FirstOrDefault();
            var ca = currentAccounts.Find(cac => cac.currentAccountId == a.currentAccountId);
            var sa = savingsAccounts.Find(sac => sac.savingsAccountId == a.savingsAccountId);
            var ac = new List<accountDetails>
            {
                new accountDetails{accountId=ca.currentAccountId,accountType="Current Account",accountBalance=ca.currentAccountBalance},
                new accountDetails{accountId=sa.savingsAccountId,accountType="Savings Account",accountBalance=sa.savingsAccountbalance}
            };
            return ac;
        }
        public customerAccount createAccount(int id)
        {
            customerAccount a = new customerAccount
            {
                customerId = id,
                currentAccountId = (id * 100) + acid,
                savingsAccountId = (id * 100) + (acid + 1)
            };
            customeraccounts.Add(a);
            var cust = customeraccounts.Find(c => c.customerId == id);
            currentAccountDetails ca = new currentAccountDetails
            {
                currentAccountId = cust.currentAccountId,
                currentAccountBalance = 0.00
            };
            currentAccounts.Add(ca);
            savingsAccount sa = new savingsAccount
            {
                savingsAccountId = cust.savingsAccountId,
                savingsAccountbalance = 0.00
            };
            savingsAccounts.Add(sa);
            return cust;
        }

        public accountDetails getAccount(int id)
        {
            if (id % 2 != 0)
            {
                var ca = currentAccounts.Find(a => a.currentAccountId == id);
                var ac1 = new accountDetails
                {
                    accountId = ca.currentAccountId,
                    accountType = "Current Account",
                    accountBalance = ca.currentAccountBalance
                };
                return ac1;
            }
            var sa = savingsAccounts.Find(a => a.savingsAccountId == id);
            var ac = new accountDetails
            {
                accountId = sa.savingsAccountId,
                accountType = "Savings Account",
                accountBalance = sa.savingsAccountbalance
            };
            return ac;
        }

        public IEnumerable<statement> getAccountStatement(int accountId, int fromDate, int toDate)
        {
            if (fromDate != 0 || toDate != 0)
            {
                var accs = accountStatements.Find(a => a.accountId == accountId);
                var s = accs.statements;
                foreach (var n in s)
                {
                    if (n.date>=fromDate && n.date<=toDate )
                    {
                        return s;
                    }
                }
            }
            var accs1 = accountStatements.Find(a => a.accountId == accountId);
            var s1 = accs1.statements;
            foreach (var n in s1)
            {
                if (n.date > 01092020 && n.date < 30092020)
                {
                    return s1;
                }
            }
            return null;
        }

        public accountDetails deposit(transactionInput value)
        {
            string data = JsonConvert.SerializeObject(value);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Transaction/deposit/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                Ts s1 = JsonConvert.DeserializeObject<Ts>(data1);
                if (s1.Message == "Success")
                {
                    if (value.accountId % 2 == 0)
                    {
                        var sa = savingsAccounts.Find(a => a.savingsAccountId == value.accountId);
                        sa.savingsAccountbalance = sa.savingsAccountbalance + value.balance;
                        accountDetails sob = new accountDetails
                        {
                            accountId = value.accountId,
                            accountType = "Deposited Correctly",
                            accountBalance = sa.savingsAccountbalance
                        };
                        return sob;
                    }
                    var ca = currentAccounts.Find(a => a.currentAccountId == value.accountId);
                    ca.currentAccountBalance = ca.currentAccountBalance + value.balance;
                    accountDetails cob = new accountDetails
                    {
                        accountId = value.accountId,
                        accountType = "Deposited Correctly",
                        accountBalance = ca.currentAccountBalance
                    };
                    return cob;
                }
            }
            return null;
        }

        public accountDetails withdraw(transactionInput value)
        {
            string data = JsonConvert.SerializeObject(value);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Transaction/withdraw/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                Ts s1 = JsonConvert.DeserializeObject<Ts>(data1);
                accountDetails amsg = new accountDetails();
                if (s1.Message == "No Warning")
                {
                    if (value.accountId % 2 == 0)
                    {
                        var sa = savingsAccounts.Find(a => a.savingsAccountId == value.accountId);
                        sa.savingsAccountbalance = sa.savingsAccountbalance - value.balance;
                        if (sa.savingsAccountbalance >= 0)
                        {
                            amsg.accountId = value.accountId;
                            amsg.accountType = "Withdrawn Successfully";
                            amsg.accountBalance = sa.savingsAccountbalance;
                            return amsg;
                        }
                        else
                        {
                            sa.savingsAccountbalance = sa.savingsAccountbalance + value.balance;
                            amsg.accountId = value.accountId;
                            amsg.accountType = "Insufficient Fund";
                            amsg.accountBalance = sa.savingsAccountbalance;
                            return amsg;
                        }
                    }
                    var car = currentAccounts.Find(a => a.currentAccountId == value.accountId);
                    car.currentAccountBalance = car.currentAccountBalance - value.balance;
                    if (car.currentAccountBalance >= 0)
                    {
                        amsg.accountId = value.accountId;
                        amsg.accountType = "Withdrawn Successfully";
                        amsg.accountBalance = car.currentAccountBalance;
                        return amsg;
                    } 
                    else
                    {
                        car.currentAccountBalance = car.currentAccountBalance + value.balance;
                        amsg.accountId = value.accountId;
                        amsg.accountType = "Insufficient Fund";
                        amsg.accountBalance = car.currentAccountBalance;
                        return amsg;
                    }

                }
                if (value.accountId % 2 == 0)
                {
                    var sa = savingsAccounts.Find(a => a.savingsAccountId == value.accountId);
                    sa.savingsAccountbalance = sa.savingsAccountbalance - value.balance;
                    if (sa.savingsAccountbalance >= 0)
                    {
                        amsg.accountId = value.accountId;
                        amsg.accountType = "Withdrawn Successfully.Service charge applicable at the end of month";
                        amsg.accountBalance = sa.savingsAccountbalance;
                        return amsg;
                    }
                    else
                    {
                        sa.savingsAccountbalance = sa.savingsAccountbalance + value.balance;
                        amsg.accountId = value.accountId;
                        amsg.accountType = "Insufficient Fund";
                        amsg.accountBalance = sa.savingsAccountbalance;
                        return amsg;
                    }
                }
                var ca = currentAccounts.Find(a => a.currentAccountId == value.accountId);
                ca.currentAccountBalance = ca.currentAccountBalance - value.balance;
                if (ca.currentAccountBalance >= 0)
                {
                    amsg.accountId = value.accountId;
                    amsg.accountType = "Withdrawn Successfully.Service Charge Applicable at the end of month";
                    amsg.accountBalance = ca.currentAccountBalance;
                    return amsg;
                }    
                else
                {
                    ca.currentAccountBalance = ca.currentAccountBalance + value.balance;
                    amsg.accountId = value.accountId;
                    amsg.accountType = "Insufficient Fund";
                    amsg.accountBalance = ca.currentAccountBalance;
                    return amsg;
                }
            }
            return null;
        }

        public transactionStatusDetails transfer(transfers value)
        {
            double sb = 0.0, db = 0.0;
            string data = JsonConvert.SerializeObject(value);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Transaction/transfer/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                Ts s1 = JsonConvert.DeserializeObject<Ts>(data1);
                transactionStatusDetails ob = new transactionStatusDetails();
                if (s1.Message == "No Warning")
                {
                    if (value.senderAccountId % 2 == 0)
                    {
                        var sas = savingsAccounts.Find(a => a.savingsAccountId == value.senderAccountId);
                        sas.savingsAccountbalance = sas.savingsAccountbalance - value.amount;
                        if (sas.savingsAccountbalance >= 0)
                            sb = sas.savingsAccountbalance;
                        else
                        {
                            sas.savingsAccountbalance = sas.savingsAccountbalance + value.amount;
                            return null;
                        }
                    }
                    else
                    {
                        var cas = currentAccounts.Find(a => a.currentAccountId == value.senderAccountId);
                        cas.currentAccountBalance = cas.currentAccountBalance - value.amount;
                        if (cas.currentAccountBalance >= 0)
                            sb = cas.currentAccountBalance;
                        else
                        {
                            cas.currentAccountBalance = cas.currentAccountBalance + value.amount;
                            return null;
                        }

                    }
                    if (value.receiverAccountId % 2 == 0)
                    {
                        var sa = savingsAccounts.Find(a => a.savingsAccountId == value.receiverAccountId);
                        sa.savingsAccountbalance = sa.savingsAccountbalance + value.amount;
                        db = sa.savingsAccountbalance;
                    }
                    else
                    {
                        var ca = currentAccounts.Find(a => a.currentAccountId == value.receiverAccountId);
                        ca.currentAccountBalance = ca.currentAccountBalance + value.amount;
                        db = ca.currentAccountBalance;
                    }
                    ob.senderBalance = sb;
                    ob.receiverBalance = db;
                    ob.transferStatus = "Transfer Successfull";
                    return ob;
                }
                else
                {
                    if (value.senderAccountId % 2 == 0)
                    {
                        var sas = savingsAccounts.Find(a => a.savingsAccountId == value.senderAccountId);
                        sas.savingsAccountbalance = sas.savingsAccountbalance - value.amount;
                        if (sas.savingsAccountbalance >= 0)
                            sb = sas.savingsAccountbalance;
                        else
                        {
                            sas.savingsAccountbalance = sas.savingsAccountbalance + value.amount;
                            return null;
                        }

                    }
                    else
                    {
                        var cas = currentAccounts.Find(a => a.currentAccountId == value.senderAccountId);
                        cas.currentAccountBalance = cas.currentAccountBalance - value.amount;
                        if (cas.currentAccountBalance >= 0)
                            sb = cas.currentAccountBalance;
                        else
                        {
                            cas.currentAccountBalance = cas.currentAccountBalance + value.amount;
                            return null;
                        }

                    }
                    if (value.receiverAccountId % 2 == 0)
                    {
                        var sa = savingsAccounts.Find(a => a.savingsAccountId == value.receiverAccountId);
                        sa.savingsAccountbalance = sa.savingsAccountbalance + value.amount;
                        db = sa.savingsAccountbalance;
                    }
                    else
                    {
                        var ca = currentAccounts.Find(a => a.currentAccountId == value.receiverAccountId);
                        ca.currentAccountBalance = ca.currentAccountBalance + value.amount;
                        db = ca.currentAccountBalance;
                    }
                    ob.senderBalance = sb;
                    ob.receiverBalance = db;
                    ob.transferStatus = "Transfer Successfull.But Service Charge is applicable in sender account";
                    return ob;
                    //return "Sender Account Balance Rs." + sb + ".00\n" + "Receiver Account Balance Rs." + db + ".00\n but service charge will be deducted at the end of month from your account";

                }

            }
            return null;
        }
    }
    
}
