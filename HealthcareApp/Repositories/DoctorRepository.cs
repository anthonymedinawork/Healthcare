using Data.Models;
using HealthcareApp.Data;
using HealthcareApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HealthcareAppDbContext _context)
            : base(_context)
        {
        }

        public async Task<Doctor> GetByUserAccountIdAsync(string id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(p => p.UserAccountId == id);
        }
    }
}
