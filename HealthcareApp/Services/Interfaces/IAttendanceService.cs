using HealthcareApp.Services.ViewModels;

namespace HealthcareApp.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task CreateAsync(AttendanceViewModel attendance);
        Task UpdateAsync(AttendanceViewModel attendance);
        Task DeleteAsync(string id);
        Task<AttendanceViewModel> GetByIdAsync(string id);
        Task<List<AttendanceViewModel>> GetAllAsync();
    }
}
