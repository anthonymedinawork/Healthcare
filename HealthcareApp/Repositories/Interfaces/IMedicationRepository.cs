using HealthcareApp.Data.Entities;

namespace HealthcareApp.Repositories.Interfaces
{
    public interface IMedicationRepository : IBaseRepository<Medication>
    {
        Task CreateManyAsync(List<Medication> meds);
        Task DeleteTableDataAsync();
    }
}
