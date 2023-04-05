using HealthcareApp.Data;
using HealthcareApp.Data.Entities;
using HealthcareApp.Repositories.Interfaces;

namespace HealthcareApp.Repositories
{
    public class MedicationRepository : BaseRepository<Medication>, IMedicationRepository
    {
        public MedicationRepository(HealthcareAppDbContext _context)
            : base(_context)
        {
        }
    }
}
