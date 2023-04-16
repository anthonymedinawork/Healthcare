using AutoMapper;
using Data.Models;
using HealthcareApp.Data;
using HealthcareApp.Repositories.Interfaces;
using HealthcareApp.Services.Interfaces;
using HealthcareApp.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthcareApp.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;

        public DoctorService(IDoctorRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task CreateAsync(DoctorViewModel model)
        {
            Doctor doctor = _mapper.Map<Doctor>(model);

            doctor.Id = Guid.NewGuid().ToString();

            await _repository.CreateAsync(doctor);
        }

        public async Task DeleteAsync(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                throw new KeyNotFoundException(nameof(id));
            }
            await _repository.DeleteAsync(id);
        }

        public async Task<List<DoctorViewModel>> GetAllAsync()
        {
            var doctors = await _repository.GetAll().ToListAsync();

            return _mapper.Map<List<DoctorViewModel>>(doctors);
        }

        public async Task<List<DoctorViewModel>> GetAllAsync(Expression<Func<DoctorViewModel, bool>> filter)
        {
            var doctorFilter = _mapper.Map<Expression<Func<Doctor, bool>>>(filter);

            List<Doctor> doctors = await _repository.GetAll(doctorFilter).ToListAsync();

            return _mapper.Map<List<DoctorViewModel>>(doctors);
        }

        public async Task<DoctorViewModel> GetByIdAsync(string id)
        {
            Doctor? doctor = await _repository.GetByIdAsync(id);

            if (doctor is null)
            {
                throw new ArgumentNullException("No such doctor exists!");
            }
            return _mapper.Map<DoctorViewModel>(doctor);
        }

        public async Task UpdateAsync(DoctorViewModel model)
        {
            Doctor doctor = _mapper.Map<Doctor>(model);

            await _repository.UpdateAsync(doctor);
        }
    }
}
