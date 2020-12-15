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
    public class DemandController : Controller
    {
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(DemandController));
        private readonly IDemandProvider _demandProvider;
        private HttpResponseMessage _response;
        private string _token;

        public DemandController(IDemandProvider demandProvider)
        {
            _demandProvider = demandProvider;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                if (HttpContext.Session.GetString("token") == null)
                {
                    _log.Info("token not found");
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    _log.Info("Displaying Schedule input page");
                    _token = HttpContext.Session.GetString("token");
                    _response = await _demandProvider.GetStock(_token);
                    if(_response.IsSuccessStatusCode)
                    {
                        _log.Info("stock received");
                        var result = await _response.Content.ReadAsStringAsync();
                        List<MedicineStock> stocks = JsonConvert.DeserializeObject<List<MedicineStock>>(result);
                        List<MedicineDemand> demands = _demandProvider.GetDemand(stocks);
                        return View(demands);
                    }
                    else if(_response.StatusCode==HttpStatusCode.NotFound)
                    {
                        _log.Error("error while getting stock");
                        return View("NoStock");
                    }
                    else if(_response.StatusCode==HttpStatusCode.Unauthorized)
                    {
                        return View("Unauthorized");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch (Exception e)
            {
                _log.Info("Error in DemandController while displaying input page for user : "+HttpContext.Session.GetString("userName")+" - "+e.Message);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(IEnumerable<MedicineDemand> demands)
        {
            try
            {
                if (HttpContext.Session.GetString("token") == null)
                {
                    _log.Info("token not found");
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    _token = HttpContext.Session.GetString("token");
                    _response = await _demandProvider.GetSupply(demands.ToList(),_token);
                    if (_response.IsSuccessStatusCode)
                    {
                        _log.Info("Supply received");
                        var result = await _response.Content.ReadAsStringAsync();
                        TempData["supply"] = result;
                        return RedirectToAction("DisplaySupply");
                    }
                    else if(_response.StatusCode==HttpStatusCode.NotFound)
                    {
                        _log.Error("error while getting supply");
                        return View("NoStock");
                    }
                    else if (_response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return View("Unauthorized");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("Error while getting demand list in DemandController for user : "+HttpContext.Session.GetString("userName")+" - "+e.Message);
                return View("Error");
            }
        }

        public IActionResult DisplaySupply()
        {
            try
            {
                if (HttpContext.Session.GetString("token") == null)
                {
                    _log.Info("token not found");
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    _log.Info("Displaying Supply List");
                    List<Supply> supplies = JsonConvert.DeserializeObject<List<Supply>>(TempData["supply"].ToString());
                    _demandProvider.AddSupplyToDB(supplies);
                    return View(supplies.OrderBy(s => s.PharmacyName));
                }
            }
            catch (Exception e)
            {
                _log.Error("Error in Demand Controller while displaying Supply for user : "+ HttpContext.Session.GetString("userName") + " - " + e.Message);
                return View("Error");
            }
        }
    }
}