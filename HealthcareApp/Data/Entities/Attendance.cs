using HealthcareApp.Data.Entities;
using System.ComponentModel.DataAnnotations;

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
