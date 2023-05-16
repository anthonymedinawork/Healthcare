using HealthcareApp.Services.ViewModels;

namespace HealthcareApp.Services.Interfaces
{
    public interface IDoctorService
    {
        Task CreateAsync(DoctorViewModel doctor);
        Task UpdateAsync(DoctorViewModel doctor);
        Task DeleteAsync(string id);
        Task<DoctorViewModel> GetByIdAsync(string id);
        Task<DoctorViewModel> GetByUserAccountIdAsync(string id);
        Task<List<DoctorViewModel>> GetAllAsync();
    }
}
