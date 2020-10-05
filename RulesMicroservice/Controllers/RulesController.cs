using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RulesMicroservice.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RulesMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(RulesController));
        IRulesRepository rulesRepository;
        public RulesController(IRulesRepository _rulesRepository)
        {
            rulesRepository = _rulesRepository;
        }
        // GET api/<RulesController>/5
        [HttpGet]
        [Route("deductServiceChargeCurrent")]
        public IActionResult evaluateMinBalCurrent()
        {
            _log4net.Info("Minimum Balance for current Account Initiated");
            try
            {
                var ob = rulesRepository.evaluateMinBalCurrent();
                if(ob==null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("deductServiceChargeSavings")]
        public IActionResult evaluateMinBalSavings()
        {
            _log4net.Info("Minimum Balance for current Account Initiated");
            try
            {
                var ob = rulesRepository.evaluateMinBalSavings();
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
            //return ls1;
        }
        [HttpGet]
        [Route("showServiceCharges")]
        public IActionResult getServiceCharges()
        {
            _log4net.Info("Service Charges logged");
            try
            {
                var ob = rulesRepository.getServiceCharges();
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
        [Route("evaluateMinBal")]
        public IActionResult evaluateMinBal([FromBody] transactionInput value)
        {
            //dwacc ob = new dwacc { AccountId = id, Balance = bid };
            _log4net.Info("Minimum Balance checking initiated");
            try
            {
                var obj = rulesRepository.evaluateMinBal(value);
                _log4net.Info("Minimum balance response generated");
                if (obj == null)
                {
                    return NotFound();
                }
                return Ok(obj);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
