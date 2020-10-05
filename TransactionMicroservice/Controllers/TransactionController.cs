using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TransactionMicroservice.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransactionMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TransactionController));
        ITransactionRepository transactionRepository;
        public TransactionController(ITransactionRepository _transactionRepository)
        {
            transactionRepository = _transactionRepository;
        }
        [HttpPost]
        [Route("deposit")]
        public IActionResult deposit([FromBody]transactionInput value)
        {
            _log4net.Info("Deposition checking initiated");
            try
            {
                var ob = transactionRepository.deposit(value);
                if(ob==null)
                {
                    _log4net.Info("Deposition Failure");
                    return NotFound();
                }
                _log4net.Info("Deposition success");
                return Ok(ob);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("withdraw")]
        public IActionResult withdraw([FromBody] transactionInput value)
        {
            _log4net.Info("Withdrawing checking initiated");
            try
            {
                var ob = transactionRepository.withdraw(value);
                if (ob == null)
                {
                    _log4net.Info("Withdrawn deny response from Rules Microservice");
                    return NotFound();
                }
                _log4net.Info("Withdrawn Success response from Rules Microservice");
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
            _log4net.Info("Transfer checking inititaed");
            try
            {
                var ob = transactionRepository.transfer(value);
                if (ob == null)
                {
                    _log4net.Info("Transfer not possible from Rules Microservice");
                    return NotFound();
                }
                _log4net.Info("Transfer possible from Rules Microservice");
                return Ok(ob);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
