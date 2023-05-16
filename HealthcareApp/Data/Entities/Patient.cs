namespace Data.Models
{
    public class Patient : Person
    {
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public string PersonalDoctorId { get; set; }
        public Doctor PersonalDoctor { get; set; }
        public string UserAccountId { get; set; }
        public User UserAccount { get; set; }
    }
}
