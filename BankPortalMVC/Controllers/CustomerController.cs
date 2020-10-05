using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BankPortalMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BankPortalMVC.Controllers
{
    public class CustomerController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CustomerController));
        ResponseDBContext db;
        Uri baseAddress = new Uri("https://localhost:44379/api");
        HttpClient client;
        public CustomerController(ResponseDBContext _db)
        {
            db = _db;
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            _log4net.Info("Index of Customer");
            string token = TokenInfo.StringToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            int id = TokenInfo.UserID;
            var accounts = new List<accountDetails>();
            ViewAccountCustRes database = new ViewAccountCustRes();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Account/getCustomerAccounts/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                accounts = JsonConvert.DeserializeObject<List<accountDetails>>(data);
            }
            try
            {
                foreach(var v in accounts)
                {
                    database.CustId = id;
                    database.AccId = v.accountId;
                    database.AccType = v.accountType;
                    database.AccBal = v.accountBalance;
                    db.viewAccountCustRes.Add(database);
                }
                db.SaveChanges();
                return View(accounts);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
        public IActionResult Deposit()
        {
            _log4net.Info("Customer initiated deposit");
            return View();
        }
        [HttpPost]
        public IActionResult Deposit(transactionInput accountBalance)
        {
            string data = JsonConvert.SerializeObject(accountBalance);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Account/deposit/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                accountDetails ob4 = JsonConvert.DeserializeObject<accountDetails>(data1);
                if (ob4 != null)
                    return RedirectToAction("DepositStatus", "Customer", ob4);
                else
                    return RedirectToAction("Error");
            }
            return BadRequest("Link Failure");
        }
        public IActionResult DepositStatus(accountDetails ob4)
        {
            _log4net.Info("Deposition status");
            var database = new ViewDepositResponse();
            try
            {
                database.AccId = ob4.accountId;
                database.AccBal = ob4.accountBalance;
                database.DepositStatus = ob4.accountType;
                db.viewDepositResponses.Add(database);
                db.SaveChanges();
                return View(ob4);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
        public IActionResult Withdraw()
        {
            _log4net.Info("Customer initiated withdraw of money");
            return View();
        }
        [HttpPost]
        public IActionResult Withdraw(transactionInput accountBalance)
        {
            string data = JsonConvert.SerializeObject(accountBalance);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Account/withdraw/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                accountDetails ob4 = JsonConvert.DeserializeObject<accountDetails>(data1);
                if (ob4 != null)
                    return RedirectToAction("WithdrawStatus", "Customer", ob4);
                else
                    return RedirectToAction("Error");
            }
            return BadRequest();
        }
        public IActionResult WithdrawStatus(accountDetails ob4)
        {
            _log4net.Info("Withdraw Status");
            var database = new ViewWithdrawResponse();
            try 
            {
                database.AccId = ob4.accountId;
                database.AccBal = ob4.accountBalance;
                database.WithdrawStatus = ob4.accountType;
                db.viewWithdrawResponses.Add(database);
                db.SaveChanges();
                return View(ob4);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
        public IActionResult transfer()
        {
            _log4net.Info("Customer initiated transfer of money");
            return View();
        }
        [HttpPost]
        public IActionResult transfer(transfers accountBalance)
        {
            string data = JsonConvert.SerializeObject(accountBalance);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Account/transfer/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                transactionmsg ob4 = JsonConvert.DeserializeObject<transactionmsg>(data1);
                if (ob4 != null)
                    return RedirectToAction("TransferStatus", "Customer", ob4);
                else
                    return RedirectToAction("Error");
            }
            return BadRequest();
        }
        public IActionResult TransferStatus(transactionmsg ob4)
        {
            _log4net.Info("Transfer Status");
            var database = new ViewTransferResponse();
            try
            {
                database.sbal = ob4.senderBalance;
                database.rbal = ob4.receiverBalance;
                database.transferStatus = ob4.transferStatus;
                db.viewTransferResponses.Add(database);
                db.SaveChanges();
                return View(ob4);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
        public IActionResult AccountStatement()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AccountStatement(accountDetailsInput accDetState)
        {
            _log4net.Info("Customer view Account Statement");
            return RedirectToAction("AccountStatementStatus", accDetState);
        }
        public IActionResult AccountStatementStatus(accountDetailsInput accDetState)
        {
            int accountId = accDetState.accountId;
            int fromDate = accDetState.fromDate;
            int toDate = accDetState.toDate;
            var database = new ViewAccountStatementResponse();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Account/getAccountStatement/" + accountId+"/"+fromDate+"/"+toDate).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<Statement> ac = JsonConvert.DeserializeObject<List<Statement>>(data);
                if(ac!=null)
                { 
                    try
                    {
                        foreach(var v in ac)
                        {
                            database.date = v.date;
                            database.Narration = v.Narration;
                            database.refno = v.refno;
                            database.valueDate = v.valueDate;
                            database.withdrawal = v.withdrawal;
                            database.deposit = v.deposit;
                            database.closingBalance = v.closingBalance;
                            db.viewAccountStatementResponses.Add(database);
                        }
                        db.SaveChanges();
                        return View(ac);
                    }
                    catch(Exception)
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            return BadRequest();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
