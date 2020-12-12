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
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(RepScheduleProvider));
        public async Task<HttpResponseMessage> GetSchedule(DateTime startDate)
        {
            try
            {
                var client = new HttpClient();
                string s = (startDate.Day).ToString() + startDate.ToString("MMM") + (startDate.Year).ToString();
                var response = await client.GetAsync("https://localhost:44387/api/RepSchedule?startdate=" + s);
                _log.Info("response received");
                return response;
            }
            catch (Exception e)
            {
                _log.Error("Error in RepScheduleProvider while getting Schedule - "+e.Message);
                throw;
            }
        }
    }
}
