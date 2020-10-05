using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AccountMicroservice.Models;
using AccountMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly log4net.ILog _log4net;
        IAccountRepository accountRepository;
        public AccountController(IAccountRepository _accountRepository)
        {
            _log4net = log4net.LogManager.GetLogger(typeof(AccountController));
            accountRepository = _accountRepository;
        }
        //int acid = 1;
        /*public static List<customeraccount> customeraccounts = new List<customeraccount>()
        {
            new customeraccount{custId=1,CAId=101,SAId=102}
        };*/
        // GET: api/<AccountController>
        [HttpGet]
        [Route("getCurrentAccountList")]
        public IActionResult GetCurrent()
        {
            try
            {
                var ob = accountRepository.getCurrent();
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("getSavingAccountList")]
        public IActionResult GetSavings()
        {
            try
            {
                var ob = accountRepository.getSavings();
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        // GET api/<AccountController>/5
        [HttpGet]
        [Route("getCustomerAccounts/{id}")]
        public IActionResult getCustomerAccounts(int id)
        {
            _log4net.Info(" Got Customer Account");
            try
            {
                var ob = accountRepository.getCustomerAccounts(id);
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<AccountController>
        [HttpPost]
        [Route("createAccount")]
        public IActionResult createAccount([FromBody] int id)
        {
            //GetListRep ob = new GetListRep();
            //var customeraccounts = ob.GetCustomeraccountsList();
            _log4net.Info("Account Created");
            try
            {
                var ob = accountRepository.createAccount(id);
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("getAccount/{id}")]
        public IActionResult getAccount(int id)
        {
            //GetListRep ob = new GetListRep();
            //var customeraccounts = ob.GetCustomeraccountsList();
            _log4net.Info(" Getting Account Info");
            try
            {
                var ob = accountRepository.getAccount(id);
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        [HttpGet]
        [Route("getAccountStatement/{AccountId}/{from_date}/{to_date}")]
        public IActionResult getAccountStatement(int AccountId, int fromdate, int todate)
        {
            _log4net.Info("Account Statement Shown");
            try
            {
                var ob = accountRepository.getAccountStatement(AccountId, fromdate, todate);
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("deposit")]
        public IActionResult deposit([FromBody] transactionInput value)
        {
            _log4net.Info(" Amount Deposited ");
            try
            {
                var ob = accountRepository.deposit(value);
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("withdraw")]
        public IActionResult withdraw([FromBody] transactionInput value)
        {
            _log4net.Info("Amount Withdrawn");
            try
            {
                var ob = accountRepository.withdraw(value);
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("transfer")]
        public IActionResult transfer([FromBody] transfers value)
        {
            _log4net.Info("Amount Transferred");
            try
            {
                var ob = accountRepository.transfer(value);
                if (ob == null)
                    return NotFound("Either Zero Balance or Link Failure");
                return Ok(ob);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
