using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Doctor : Person
    {
        [Required]
        public string Qualification { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public string UserAccountId { get; set; }
        public User UserAccount { get; set; }
    }
}
