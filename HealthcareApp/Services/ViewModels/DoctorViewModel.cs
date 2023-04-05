using Data.Models;
using System.ComponentModel.DataAnnotations;

namespace HealthcareApp.Services.ViewModels
{
    public class DoctorViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Qualification { get; set; }
        public ICollection<PatientViewModel> Patients { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
