using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacyMedicineSupply.Models;
using PharmacyMedicineSupply.Providers;

namespace PharmacyMedicineSupply.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(UserController));
        private readonly IRepScheduleProvider _repProvider;
        private string _token;
        public ScheduleController(IRepScheduleProvider repProvider)
        {
            _repProvider = repProvider;
        }

        public IActionResult Index()
        {
            try
            {
                _log.Info("Displaying Index page of Scheduling");
                string today = DateTime.Today.Year.ToString()+"-"+DateTime.Today.Month.ToString()+"-"+DateTime.Today.Day;
                ViewBag.Min = today;
                return View();
            }
            catch (Exception e)
            {
                _log.Error("Error in ScheduleController while displaying Index page - "+e.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Index(ScheduleDate dates)
        {
            try
            {
                _token = HttpContext.Session.GetString("token");
                HttpResponseMessage response = await _repProvider.GetSchedule(dates.Date,_token);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    TempData["result"] = result;
                    return RedirectToAction("Schedule");
                }
                else if(response.StatusCode==HttpStatusCode.NotFound)
                {
                    _log.Error("could not schedule");
                    return View("NoSchedule");
                }
                else if(response.StatusCode==HttpStatusCode.Unauthorized)
                {
                    return View("Unauthorized");
                }
                else
                {
                    _log.Error("Error occured in Micro-Service called for scheduling");
                    return View("Error");
                }
            }
            catch (Exception e)
            {
                _log.Error("Error in Schedule Controller while displaying schedule - "+e.Message);
                return View("Error");
            }
        }

        public IActionResult Schedule()
        {
            try
            {
                List<RepSchedule> schedules =
                    JsonConvert.DeserializeObject<List<RepSchedule>>(TempData["result"].ToString());
                _repProvider.AddToDb(schedules);
                return View(schedules);
            }
            catch (Exception e)
            {
                _log.Error(e.Message);
                return View("Error");
            }
        }
    }
}