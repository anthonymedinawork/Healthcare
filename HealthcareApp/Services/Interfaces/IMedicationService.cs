using HealthcareApp.Services.ViewModels;

namespace HealthcareApp.Services.Interfaces
{
    public interface IMedicationService
    {
        Task CreateAsync(MedicationViewModel med);
        Task UpdateAsync(MedicationViewModel med);
        Task DeleteAsync(string id);
        Task<MedicationViewModel> GetByIdAsync(string id);
        Task<List<MedicationViewModel>> GetAllAsync();
    }
}
