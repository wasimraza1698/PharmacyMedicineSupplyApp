using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyMedicineSupply.Models;

namespace PharmacyMedicineSupply.Repositories
{
    public interface IRepScheduleRepo
    {
        public bool Add(RepScheduleDto repSchedule);
    }
}
