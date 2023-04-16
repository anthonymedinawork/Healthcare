using AutoMapper;
using Data.Models;
using HealthcareApp.Data.Entities;
using HealthcareApp.Repositories.Interfaces;
using HealthcareApp.Services.Interfaces;
using HealthcareApp.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthcareApp.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly IMedicationRepository _repository;
        private readonly IMapper _mapper;

        public MedicationService(IMedicationRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task CreateAsync(MedicationViewModel model)
        {
            Medication med = _mapper.Map<Medication>(model);

            await _repository.CreateAsync(med);
        }

        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new KeyNotFoundException(nameof(id));
            }
            await _repository.DeleteAsync(id);
        }

        public async Task<List<MedicationViewModel>> GetAllAsync()
        {
            var meds = await _repository.GetAll().ToListAsync();

            return _mapper.Map<List<MedicationViewModel>>(meds);
        }

        public async Task<List<MedicationViewModel>> GetAllAsync(Expression<Func<MedicationViewModel, bool>> filter)
        {
            var medFilter = _mapper.Map<Expression<Func<Medication, bool>>>(filter);

            List<Medication> meds = await _repository.GetAll(medFilter).ToListAsync();

            return _mapper.Map<List<MedicationViewModel>>(meds);
        }

        public async Task<MedicationViewModel> GetByIdAsync(string id)
        {
            Medication? med = await _repository.GetByIdAsync(id);

            if (med is null)
            {
                throw new ArgumentNullException("No such medication exists!");
            }
            return _mapper.Map<MedicationViewModel>(med);
        }

        public async Task UpdateAsync(MedicationViewModel model)
        {
            Medication med = _mapper.Map<Medication>(model);

            await _repository.UpdateAsync(med);
        }
    }
}
