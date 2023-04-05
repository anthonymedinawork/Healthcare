using Data.Models;
using HealthcareApp.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HealthcareApp.Services.ViewModels
{
    public class AttendanceViewModel
    {
        public string Id { get; set; }
        public string DoctorId { get; set; }
        public DoctorViewModel Doctor { get; set; }
        public string Diagnosis { get; set; }
        public DateTime Date { get; set; }
        public ICollection<MedicationViewModel>? Medications { get; set; }
    }
}
