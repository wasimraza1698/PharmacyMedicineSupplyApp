using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupply.Models
{
    public class MedicineDemand
    {
        [DisplayName("Medicine")]
        public string MedicineName { get; set; }
        public int Count { get; set; }
    }
}
