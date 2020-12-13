using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            try
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
            catch (Exception e)
            {
                _log.Error("Error in UserController - "+e.Message);
                return View("Error");
            }
        }
        public IActionResult Login()
        {
            try
            {
                _log.Info("Displaying Login Page");
                return View();
            }
            catch (Exception e)
            {
                _log.Error("Error in UserController while displaying login page - "+e.Message);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User credentials)
        {
            try
            {
                HttpResponseMessage response = await  _userProvider.Login(credentials);
                if (response.IsSuccessStatusCode)
                {
                    _log.Info("success response received");
                    var result = await response.Content.ReadAsStringAsync();
                    JWT token = JsonConvert.DeserializeObject<JWT>(result);
                    HttpContext.Session.SetString("token",token.Token);
                    HttpContext.Session.SetString("userName", credentials.UserName);
                    ViewBag.UserName = credentials.UserName;
                    return View("Index");
                }
                else if(response.StatusCode == HttpStatusCode.NotFound)
                {
                    _log.Info("invalid username or password for user : "+credentials.UserName);
                    ViewBag.Info = "Invalid username/password";
                    return View();
                }
                else
                {
                    _log.Error("Error in Micro-service called for Authentication");
                    return View("Error");
                }
            }
            catch (Exception e)
            {
                _log.Error("Error in UserController while logging in for user : "+credentials.UserName+" - " + e.Message);
                return View("Error");
            }
        }
        public IActionResult Logout()
        {
            try
            {
                _log.Info("Logging out user : "+HttpContext.Session.GetString("userName"));
                HttpContext.Session.Remove("token");
                HttpContext.Session.Remove("userName");
                return View();
            }
            catch (Exception e)
            {
                _log.Error("Error in UserController while logging out for user : "+HttpContext.Session.GetString("userName")+" - " + e.Message);
                return View("Error");
            }
        }
    }
}