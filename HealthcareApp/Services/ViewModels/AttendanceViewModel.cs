namespace HealthcareApp.Services.ViewModels
{
    public class AttendanceViewModel
    {
        public string Id { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public PatientsSelectListModel PatientsList { get; set; }
        public string Diagnosis { get; set; }
        public DateTime Date { get; set; }
        public string MedicationId { get; set; }
        public string MedicationName { get; set; }
        public MedicationsSelectListModel MedicationsList { get; set; }
    }
}
