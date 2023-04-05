using Data.Models;
using HealthcareApp.Data;
using HealthcareApp.Repositories.Interfaces;

namespace HealthcareApp.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(HealthcareAppDbContext _context)
            : base(_context)
        {
        }
    }
}
