using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyMedicineSupply.Models;

namespace PharmacyMedicineSupply.Providers
{
    public interface IRepScheduleProvider
    {
        public Task<List<RepSchedule>> GetSchedule(DateTime startDate);
    }
}
