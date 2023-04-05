using Data.Models;
using HealthcareApp.Data;
using HealthcareApp.Repositories.Interfaces;

namespace HealthcareApp.Repositories
{
    public class AttendanceRepository : BaseRepository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(HealthcareAppDbContext _context)
            : base(_context)
        {
        }
    }
}
