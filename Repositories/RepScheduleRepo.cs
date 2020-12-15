using PharmacyMedicineSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyMedicineSupply.Data;

namespace PharmacyMedicineSupply.Repositories
{
    public class RepScheduleRepo : IRepScheduleRepo
    {
        private PMSContext _context;
        public RepScheduleRepo(PMSContext context)
        {
            _context = context;
        }
        public bool Add(RepScheduleDto repSchedule)
        {
            _context.Add(repSchedule);
            _context.SaveChanges();
            return true;
        }
    }
}
