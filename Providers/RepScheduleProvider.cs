using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PharmacyMedicineSupply.Models;
using PharmacyMedicineSupply.Repositories;

namespace PharmacyMedicineSupply.Providers
{
    public class RepScheduleProvider : IRepScheduleProvider
    {
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(RepScheduleProvider));
        private readonly IRepScheduleRepo _repScheduleRepo;

        public RepScheduleProvider(IRepScheduleRepo repScheduleRepo)
        {
            _repScheduleRepo = repScheduleRepo;
        }

        public void AddToDb(List<RepSchedule> schedule)
        {
            try
            {
                foreach (var s in schedule)
                {
                    RepScheduleDto repSchedule = new RepScheduleDto(){DateOfMeeting = s.DateOfMeeting,DoctorContactNumber = s.DoctorContactNumber,DoctorName = s.DoctorName,Medicine = s.Medicine,MeetingSlot = s.MeetingSlot,RepName = s.RepName,TreatingAilment = s.TreatingAilment};
                    _log.Info("Adding schedule to database");
                    _repScheduleRepo.Add(repSchedule);
                }
            }
            catch (Exception e)
            {
                _log.Error("Error while adding schedule to database");
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetSchedule(DateTime startDate,string token)
        {
            try
            {
                var client = new HttpClient();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
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
