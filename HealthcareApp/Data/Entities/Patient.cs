using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Patient : Person
    {
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public string PersonalDoctorId { get; set; }
        public Doctor PersonalDoctor { get; set; }
    }
}
