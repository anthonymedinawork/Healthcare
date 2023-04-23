using AutoMapper;
using Data.Models;
using HealthcareApp.Repositories.Interfaces;
using HealthcareApp.Services.Interfaces;
using HealthcareApp.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthcareApp.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task CreateAsync(PatientViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();

            Patient patient = _mapper.Map<Patient>(model);

            await _repository.CreateAsync(patient);
        }

        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new KeyNotFoundException(nameof(id));
            }
            await _repository.DeleteAsync(id);
        }

        public async Task<List<PatientViewModel>> GetAllAsync()
        {
            var patients = await _repository.GetAll().ToListAsync();

            return _mapper.Map<List<PatientViewModel>>(patients);
        }

        public async Task<List<PatientViewModel>> GetAllAsync(Expression<Func<PatientViewModel, bool>> filter)
        {
            var patientFilter = _mapper.Map<Expression<Func<Patient, bool>>>(filter);

            List<Patient> patients = await _repository.GetAll(patientFilter).ToListAsync();

            return _mapper.Map<List<PatientViewModel>>(patients);
        }

        public async Task<PatientViewModel> GetByIdAsync(string id)
        {
            Patient? patient = await _repository.GetByIdAsync(id);

            if (patient is null)
            {
                throw new ArgumentNullException("No such patient exists!");
            }
            return _mapper.Map<PatientViewModel>(patient);
        }

        public async Task UpdateAsync(PatientViewModel model)
        {
            Patient patient = _mapper.Map<Patient>(model);

            await _repository.UpdateAsync(patient);
        }
    }
}
