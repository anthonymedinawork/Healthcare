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

        public async Task CreateManyAsync(List<Medication> meds)
        {
            foreach (var med in meds)
            {
                _context.Set<Medication>().Add(med);
            }
            
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTableDataAsync()
        {
            _context.Set<Medication>().RemoveRange(_context.Set<Medication>());

            await _context.SaveChangesAsync();
        }
    }
}
