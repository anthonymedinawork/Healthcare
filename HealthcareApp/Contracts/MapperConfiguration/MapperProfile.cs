using AutoMapper;
using Data.Models;
using HealthcareApp.Data.Entities;
using HealthcareApp.Services.ViewModels;

namespace HealthcareApp.Contracts.MapperConfiguration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Doctor, DoctorViewModel>().ReverseMap();

            CreateMap<Patient, PatientViewModel>().ReverseMap();

            CreateMap<User, UserViewModel>().ReverseMap();

            CreateMap<Appointment, AppointmentViewModel>().ReverseMap();

            CreateMap<Attendance, AttendanceViewModel>().ReverseMap();

            CreateMap<Medication, MedicationViewModel>().ReverseMap();
        }
    }
}
