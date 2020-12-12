using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PharmacyMedicineSupply.Models;

namespace PharmacyMedicineSupply.Providers
{
    public interface IRepScheduleProvider
    {
        public Task<HttpResponseMessage> GetSchedule(DateTime startDate);
    }
}
