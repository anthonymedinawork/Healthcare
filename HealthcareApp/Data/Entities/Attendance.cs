using HealthcareApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Attendance : BaseEntity
    {
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [Required]
        public string Diagnosis { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public ICollection<Medication>? Medications { get; set; }
    }
}
