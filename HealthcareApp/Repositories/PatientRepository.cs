using Data.Models;
using HealthcareApp.Data;
using HealthcareApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
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

        public override async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                throw new ArgumentException($"No such {typeof(Patient)} with id: {id}");
            }

            _context.Remove(entity);

            await CascadeDelete(entity);

            await _context.SaveChangesAsync();
        }

        private async Task CascadeDelete(Patient entity)
        {
            var account = await _context.Set<User>().FindAsync(entity.UserAccountId);

            _context.Set<User>().Remove(account);
        }
    }
}
