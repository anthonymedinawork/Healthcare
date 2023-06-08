using Data.Models;
using HealthcareApp.Data;
using HealthcareApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

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

            _context.Set<User>().Remove(account);
        }
    }
}
