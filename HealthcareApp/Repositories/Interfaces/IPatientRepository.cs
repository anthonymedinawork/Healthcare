using Data.Models;

namespace HealthcareApp.Repositories.Interfaces
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<Patient> GetByUserAccountIdAsync(string id);
    }
}
