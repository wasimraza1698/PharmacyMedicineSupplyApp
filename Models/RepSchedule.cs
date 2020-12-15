using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupply.Models
{
    public class RepSchedule
    {
        [DisplayName("Representative")]
        public string RepName { get; set; }
        [DisplayName("Doctor")]
        public string DoctorName { get; set; }
        [DisplayName("Treating Ailment")]
        public string TreatingAilment { get; set; }
        public string Medicine { get; set; }
        [DisplayName("Slot")]
        public string MeetingSlot { get; set; }
        [DisplayName("Date")]
        public string DateOfMeeting { get; set; }
        [DisplayName("Doctor's Number")]
        public int DoctorContactNumber { get; set; }
    }
}
