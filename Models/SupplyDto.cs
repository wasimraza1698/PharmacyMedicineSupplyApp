using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupply.Models
{
    public class SupplyDto
    {
        [Key]
        public int SupplyId { get; set; }
        [Required]
        public string PharmacyName { get; set; }
        [Required]
        public string MedicineName { get; set; }
        [Required]
        public int SupplyCount { get; set; }
    }
}
