using PharmacyMedicineSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PharmacyMedicineSupply.Repositories;

namespace PharmacyMedicineSupply.Providers
{
    public class DemandProvider : IDemandProvider
    {
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(DemandProvider));

        readonly HttpClient httpClient = new HttpClient();
        private readonly ISupplyRepo _supplyRepo;

        public DemandProvider(ISupplyRepo supplyRepo)
        {
            _supplyRepo = supplyRepo;
        }
        public async Task<HttpResponseMessage> GetSupply(List<MedicineDemand> demands,string token)
        {
            try
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                StringContent content= new StringContent(JsonConvert.SerializeObject(demands),Encoding.UTF8,"application/json"); 
                var response = await httpClient.PostAsync("https://localhost:44377/PharmacySupply/Get", content);
                _log.Info("response received");
                return response;
            }
            catch (Exception e)
            {
                _log.Error("Error in DemandProvider while getting supply - "+e.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetStock(string token)
        {
            try
            {
                var response = await httpClient.GetAsync("https://localhost:44394/MedicineStockInformation");
                _log.Info("response received");
                return response;
            }
            catch (Exception e)
            {
                _log.Error("Error in DemandProvider while getting stock - " + e.Message);
                throw;
            }
        }

        public List<MedicineDemand> GetDemand(List<MedicineStock> stocks)
        {
            try
            {
                List<MedicineDemand> demands = new List<MedicineDemand>();
                foreach (var stock in stocks)
                {
                    MedicineDemand demand = new MedicineDemand(){MedicineName = stock.Name,Count = stock.NumberOfTabletsInStock};
                    demands.Add(demand);
                }
                _log.Info("stock converted to demand");
                return demands;
            }
            catch (Exception e)
            {
                _log.Error("Error while converting Stock to Demand in DemandProvider"+e.Message);
                throw;
            }
        }

        public void AddSupplyToDB(List<Supply> supplies)
        {
            try
            {
                foreach (var s in supplies)
                {
                    SupplyDto supply = new SupplyDto(){MedicineName = s.MedicineName,PharmacyName = s.PharmacyName,SupplyCount = s.SupplyCount};
                    _log.Info("Adding supplies to database");
                    _supplyRepo.Add(supply);
                }
            }
            catch (Exception e)
            {
                _log.Error("Error while adding supplies to database - "+e.Message);
                throw;
            }
        }
    }
}
