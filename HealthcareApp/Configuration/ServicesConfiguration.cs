using HealthcareApp.Repositories.Interfaces;
using HealthcareApp.Repositories;
using HealthcareApp.Services.Interfaces;
using HealthcareApp.Services;
using Microsoft.AspNetCore.Authentication;

namespace HealthcareApp.Configuration
{
    public static class ServicesConfiguration
    {
        public static void AddCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IAttendanceService, AttendanceService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IMedicationService, MedicationService>();
        }
        public static void AddCustomRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IMedicationRepository, MedicationRepository>();
        }
    }
}
