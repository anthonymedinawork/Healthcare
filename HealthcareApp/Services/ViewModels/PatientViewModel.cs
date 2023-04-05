using Data.Models;

namespace HealthcareApp.Services.ViewModels
{
    public class PatientViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<AppointmentViewModel> Appointments { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public string PersonalDoctorId { get; set; }
        public DoctorViewModel PersonalDoctor { get; set; }
    }
}
