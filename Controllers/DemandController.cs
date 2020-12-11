using System;
using System.Collections.Generic;
using System.Linq;
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

        public DemandController(IDemandProvider demandProvider)
        {
            _demandProvider = demandProvider;
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log.Info("token not found");
                return RedirectToAction("Login", "User");
            }
            else
            {
                List<MedicineStock> stocks = new List<MedicineStock>();
                List<MedicineDemand> demands = new List<MedicineDemand>();
                _log.Info("Displaying Schedule input page");
                response = await _demandProvider.GetStock();
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    stocks = JsonConvert.DeserializeObject<List<MedicineStock>>(result);
                    demands = _demandProvider.GetDemand(stocks);
                    return View(demands);
                }
                else
                {
                    return View("NoStock");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(IEnumerable<MedicineDemand> demands)
        {
            response =await _demandProvider.GetSupply(demands.ToList());
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                List<Supply> supplies = JsonConvert.DeserializeObject<List<Supply>>(result);
                return RedirectToAction("DisplaySupply");
            }
            else
            {
                return View("NoStock");
            }
            
        }

        public IActionResult DisplaySupply(List<Supply> supplies)
        {
            return View(supplies);
        }
    }
}