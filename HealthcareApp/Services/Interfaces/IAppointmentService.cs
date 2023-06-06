using HealthcareApp.Services.ViewModels;
using System.Linq.Expressions;

namespace HealthcareApp.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task CreateAsync(string requesterName, AppointmentViewModel appointment);
        Task UpdateAsync(AppointmentViewModel appointment);
        Task DeleteAsync(string id);
        Task<AppointmentViewModel> GetByIdAsync(string id);
        Task<List<AppointmentViewModel>> GetAllAsync(string requesterName);
        Task<List<AppointmentViewModel>> GetAllAsync(Expression<Func<AppointmentViewModel, bool>> filter);
    }
}
