namespace HealthcareApp.Services.ViewModels
{
    public class AppointmentViewModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public bool IsApproved { get; set; }
    }
}
