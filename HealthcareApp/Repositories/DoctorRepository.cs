using Data.Models;
using HealthcareApp.Data;
using HealthcareApp.Repositories.Interfaces;

namespace HealthcareApp.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HealthcareAppDbContext _context)
            : base(_context)
        {
        }
    }
}
