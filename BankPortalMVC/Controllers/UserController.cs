using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BankPortalMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BankPortalMVC.Controllers
{
    public class UserController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(UserController));
        Uri baseAddress = new Uri("https://localhost:44373/api");
        HttpClient client;
        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Login()
        {
            _log4net.Info("Login Initiated");
            return View();
        }
        public IActionResult Auth(User user)
        {
            _log4net.Info("Authentication Inititaed");
            string content = JsonConvert.SerializeObject(user);
            StringContent data = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            try
            {
                response = client.PostAsync(client.BaseAddress + "/Token", data).Result;
            }
            catch
            {
                return RedirectToAction("Error");
            }
            string token = response.Content.ReadAsStringAsync().Result;
            if (token != "error")
            {
                _log4net.Info("Token Generated");
                TokenInfo.StringToken = token;
                TokenInfo.UserID = user.UserID;
                if (user.UserID % 2 == 0)
                    return RedirectToAction("Index", "Customer");
                else
                    return RedirectToAction("Index", "Employee");
            }
            _log4net.Info("Token Not Generated");
            TokenInfo.StringToken = null;
            TokenInfo.UserID = 0;
            return RedirectToAction("Error");
        }
        public IActionResult Logout()
        {
            _log4net.Info("Logout of System");
            TokenInfo.StringToken = null;
            TokenInfo.UserID = 0;
            return RedirectToAction("Login");
        }
        public ActionResult Error()
        {
            _log4net.Info("Error Occured");
            return View();
        }
    }
}
