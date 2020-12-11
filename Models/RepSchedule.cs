using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupply.Models
{
    public class RepSchedule
    {
        public string Rep_Name { get; set; }
        public string Doctor_Name { get; set; }
        public string Meeting_Slot { get; set; }
        public string Date_Of_Meeting { get; set; }
        public string Medicine { get; set; }
        public int Doctor_Contact_Number { get; set; }
        public string Treating_Ailment { get; set; }
    }
}
