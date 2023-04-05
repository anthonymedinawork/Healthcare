using Data.Models;
using System.ComponentModel.DataAnnotations;

namespace HealthcareApp.Services.ViewModels
{
    public class AppointmentViewModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string PatientId { get; set; }
        public PatientViewModel Patient { get; set; }
        public string DoctorId { get; set; }
        public DoctorViewModel Doctor { get; set; }
        public bool IsApproved { get; set; }
    }
}
