using Data.Models;
using HealthcareApp.Services.ViewModels;

namespace HealthcareApp.Repositories.Interfaces
{
    public interface IDoctorRepository : IBaseRepository<Doctor>
    {
        Task<Doctor> GetByUserAccountIdAsync(string id);
    }
}
