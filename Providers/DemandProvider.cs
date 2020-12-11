using PharmacyMedicineSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PharmacyMedicineSupply.Providers
{
    public class DemandProvider : IDemandProvider
    {
        HttpClient httpClient = new HttpClient();
        public async Task<HttpResponseMessage> GetSupply(List<MedicineDemand> demands)
        {
            StringContent content= new StringContent(JsonConvert.SerializeObject(demands),Encoding.UTF8,"application/json"); 
            var response = await httpClient.PostAsync("",content);
            return response;
        }

        public async Task<HttpResponseMessage> GetStock()
        {
            var response = await httpClient.GetAsync("");
            return response;
        }

        public List<MedicineDemand> GetDemand(List<MedicineStock> stocks)
        {
            List<MedicineDemand> demands = new List<MedicineDemand>();
            foreach (var stock in stocks)
            {
                MedicineDemand demand = new MedicineDemand(){MedicineName = stock.Name,Count = stock.Number_Of_Tablets_In_Stock};
                demands.Add(demand);
            }
            return demands;
        }
    }
}
