using Data.Models;
using HealthcareApp.Data;
using HealthcareApp.Repositories.Interfaces;

namespace HealthcareApp.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(HealthcareAppDbContext _context)
            : base(_context)
        {
        }
    }
}
