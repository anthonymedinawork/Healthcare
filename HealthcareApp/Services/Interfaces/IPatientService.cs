using HealthcareApp.Services.ViewModels;

namespace HealthcareApp.Services.Interfaces
{
    public interface IPatientService
    {
        Task CreateAsync(PatientViewModel patient);
        Task UpdateAsync(PatientViewModel patient);
        Task DeleteAsync(string id);
        Task<PatientViewModel> GetByIdAsync(string id);
        Task<PatientViewModel> GetByUserAccountIdAsync(string id);
        Task<List<PatientViewModel>> GetAllAsync(string requesterName);
    }
}
