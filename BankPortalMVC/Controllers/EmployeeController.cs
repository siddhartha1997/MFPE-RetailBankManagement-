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
    public class EmployeeController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(EmployeeController));
        HttpClient client;
        ResponseDBContext db;
        public EmployeeController(ResponseDBContext _db)
        {
            db = _db;
            client = new HttpClient();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCustomer(customerDetails customer)
        {
            _log4net.Info("New Customer Creation Initiated");
            string token = TokenInfo.StringToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string data = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("https://localhost:44377/api/Customer/createCustomer", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                CustomerCreationStatus ob4 = JsonConvert.DeserializeObject<CustomerCreationStatus>(data1);
                if (ob4 != null)
                    return RedirectToAction("CreationStatus", ob4);
                else
                    return RedirectToAction("Error");
            }
            return BadRequest();
        }
        public IActionResult CreationStatus(CustomerCreationStatus ob4)
        {
            _log4net.Info("New Customer Creation Response");
            var database = new ViewNewCustomerResponse();
            try 
            {
                database.CustomerId = ob4.customerId;
                database.CurrentAccountId = ob4.currentAccountId;
                database.SavingsAccountId = ob4.savingsAccountId;
                database.Message = ob4.Message;
                db.viewNewCustomerResponses.Add(database);
                db.SaveChanges();
                return View(ob4); 
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
        public IActionResult GetCustomerAccountDetails()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetCustomerAccountDetails(customerId cid)
        {
            _log4net.Info("Details of a particular customer initiated");
            return RedirectToAction("AccountStatus", cid);
            /*int acid = cid.id;
            //var ac = new List<AccountMsg>();
            HttpResponseMessage response = client.GetAsync("https://localhost:44379/api/Account/getCustomerAccounts/" + acid).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<AccountMsg> ac = JsonConvert.DeserializeObject<List<AccountMsg>>(data);
                return RedirectToAction("AccountStatus", ac);
            }
            return BadRequest();*/
        }
        public IActionResult AccountStatus(customerId custId)
        {
            //List<AccountMsg> abc = ac;
            _log4net.Info("Customer Details generated");
            int clientId = custId.id;
            var database = new ViewAccountDetailsResponse();
            HttpResponseMessage response = client.GetAsync("https://localhost:44379/api/Account/getCustomerAccounts/" + clientId).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<accountDetails> ac = JsonConvert.DeserializeObject<List<accountDetails>>(data);
                if (ac != null)
                {
                    try
                    {
                        foreach (var v in ac)
                        {
                            database.AccId = v.accountId;
                            database.AccBal = v.accountBalance;
                            database.AccType = v.accountType;
                            db.viewAccountDetailsResponses.Add(database);
                        }
                        db.SaveChanges();
                        return View(ac);
                    }
                    catch (Exception)
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            return BadRequest("Link Failure");
        }
        public IActionResult CurrentAccountChecking()
        {
            var database = new ViewCurrentServiceResponse();
            HttpResponseMessage response = client.GetAsync("https://localhost:44356/api/Rules/deductServiceChargeCurrent").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<RulesMsg> ac = JsonConvert.DeserializeObject<List<RulesMsg>>(data);
                if (ac != null)
                {
                    try
                    {
                        foreach (var v in ac)
                        {
                            database.AccId = v.accountId;
                            database.AccBal = v.accountBalance;
                            database.Message = v.message;
                            db.viewCurrentServiceResponses.Add(database);
                        }
                        db.SaveChanges();
                        return View(ac);
                    }
                    catch (Exception)
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            return BadRequest("Link Failure");
        }
        public IActionResult SavingsAccountChecking()
        {
            var database = new ViewSavingsServiceResponse();
            HttpResponseMessage response = client.GetAsync("https://localhost:44356/api/Rules/deductServiceChargeSavings").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<RulesMsg> ac = JsonConvert.DeserializeObject<List<RulesMsg>>(data);
                if(ac!=null)
                { 
                    try
                    {
                        foreach(var v in ac)
                        {
                            database.AccId = v.accountId;
                            database.AccBal = v.accountBalance;
                            database.Message = v.message;
                            db.viewSavingsServiceResponses.Add(database);
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
            return BadRequest("Link Failure");
        }
        public IActionResult Error()
        {
            _log4net.Info("Error Occured");
            return View();
        }
    }
}
