using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupply.Models
{
    public class Supply
    {
        [DisplayName("Pharmacy")]
        public string PharmacyName { get; set; }
        [DisplayName("Medicine")]
        public string MedicineName { get; set; }
        [DisplayName("Count")]
        public int SupplyCount { get; set; }
    }
}
