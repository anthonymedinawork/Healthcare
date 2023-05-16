using Data.Models;

namespace HealthcareApp.Services.ViewModels
{
    public class PatientViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserAccountId { get; set; }
        public DoctorsSelectListModel DoctorsList { get; set; }
        public string PersonalDoctorId { get; set; }
    }
}
