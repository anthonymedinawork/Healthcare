using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Appointment : BaseEntity
    {
        [Required]
        public DateTime Date { get; set; }

        public string PatientId { get; set; }
        public Patient Patient { get; set; }
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [Required]
        public bool IsApproved { get; set; }
    }
}
