using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyMedicineSupply.Models;

namespace PharmacyMedicineSupply.Repositories
{
    public interface ISupplyRepo
    {
        public bool Add(SupplyDto supply);
    }
}
