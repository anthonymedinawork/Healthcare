using Data.Models;
using HealthcareApp.Data;
using HealthcareApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(HealthcareAppDbContext _context)
            : base(_context)
        {
        }
        public async Task<Patient> GetByUserAccountIdAsync(string id)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.UserAccountId == id);
        }
    }
}
