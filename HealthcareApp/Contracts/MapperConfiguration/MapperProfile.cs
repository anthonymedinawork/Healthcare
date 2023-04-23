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

            CreateMap<Appointment, AppointmentViewModel>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));

            CreateMap<Attendance, AttendanceViewModel>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));

            CreateMap<Medication, MedicationViewModel>().ReverseMap()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));
        }
    }
}
