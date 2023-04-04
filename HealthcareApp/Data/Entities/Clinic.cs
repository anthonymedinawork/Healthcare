using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Clinic
    {
        [Key]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

        [Required]
        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }
}
