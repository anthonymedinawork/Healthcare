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

        public override async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                throw new ArgumentException($"No such {typeof(Doctor)} with id: {id}");
            }

            _context.Remove(entity);

            await CascadeDelete(entity);

            await _context.SaveChangesAsync();
        }

        private async Task CascadeDelete(Doctor entity)
        {
            var account = await _context.Set<User>().FindAsync(entity.UserAccountId);

            var appointments = await _context.Set<Appointment>()
                .Where(a => a.DoctorId == entity.Id)
                .ToListAsync();

            var attendances = await _context.Set<Attendance>()
                .Where(a => a.DoctorId == entity.Id)
                .ToListAsync();

            var patients = await _context.Set<Patient>()
                .Where(p => p.PersonalDoctorId == entity.Id)
                .ToListAsync();

            foreach (var patient in patients)
            {
                var patientAccount = await _context.Set<User>().FindAsync(patient.UserAccountId);

                _context.Set<User>().Remove(patientAccount);
            }

            appointments.ForEach(a => _context.Set<Appointment>().Remove(a));

            attendances.ForEach(a => _context.Set<Attendance>().Remove(a));

            patients.ForEach(p => _context.Set<Patient>().Remove(p));

            _context.Set<User>().Remove(account);
        }
    }
}
