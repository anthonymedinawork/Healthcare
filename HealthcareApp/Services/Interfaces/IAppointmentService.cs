using HealthcareApp.Services.ViewModels;

namespace HealthcareApp.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task CreateAsync(AppointmentViewModel appointment);
        Task UpdateAsync(AppointmentViewModel appointment);
        Task DeleteAsync(string id);
        Task<AppointmentViewModel> GetByIdAsync(string id);
        Task<List<AppointmentViewModel>> GetAllAsync();
    }
}
