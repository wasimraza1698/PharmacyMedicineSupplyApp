using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PharmacyMedicineSupply.Models;

namespace PharmacyMedicineSupply.Providers
{
    public class RepScheduleProvider : IRepScheduleProvider
    {
        List<RepSchedule> res = new List<RepSchedule>();
        public async Task<List<RepSchedule>> GetSchedule(DateTime startDate)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44387/");
            string s = (startDate.Day).ToString() + startDate.ToString("MMM") + (startDate.Year).ToString();
            var response = await client.GetAsync("api/RepSchedule?startdate=" + s);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<List<RepSchedule>>(jsonString);

            }
            return res;
        }
    }
}
