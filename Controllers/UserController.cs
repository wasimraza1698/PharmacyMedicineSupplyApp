using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacyMedicineSupply.Models;
using PharmacyMedicineSupply.Providers;

namespace PharmacyMedicineSupply.Controllers
{
    public class UserController : Controller
    {
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(UserController));
        private readonly IUserProvider _userProvider;

        public UserController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }
        
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log.Info("token not found");
                return RedirectToAction("Login");
            }
            else
            {
                _log.Info("Displaying Home Page");
                return View();
            }
        }
        public IActionResult Login()
        {
            _log.Info("Displaying Login Page");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User credentials)
        {
            HttpResponseMessage response = await  _userProvider.Login(credentials);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                JWT token = JsonConvert.DeserializeObject<JWT>(result);
                HttpContext.Session.SetString("token",token.Token);
                HttpContext.Session.SetString("userName", credentials.UserName);
                ViewBag.UserName = credentials.UserName;
                return View("Index");
            }
            else
            {
                ViewBag.Info = "Invalid username/password";
                return View();
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("userName");
            return View();
        }
    }
}