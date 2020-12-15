using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupply.Models
{
    public class RepScheduleDto
    {
        [Key]
        public int ScheduleId { get; set; }
        [Required]
        public string RepName { get; set; }
        [Required]
        public string DoctorName { get; set; }
        [Required]
        public string TreatingAilment { get; set; }
        [Required]
        public string Medicine { get; set; }
        [Required]
        public string MeetingSlot { get; set; }
        [Required]
        public string DateOfMeeting { get; set; }
        [Required]
        public int DoctorContactNumber { get; set; }
    }
}
