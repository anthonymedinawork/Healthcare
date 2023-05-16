using AutoMapper;
using Data.Models;
using HealthcareApp.Data.Enums;
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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public DoctorService(IDoctorRepository repository, IMapper mapper, IUserService userService)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._userService = userService;
        }

        public async Task CreateAsync(DoctorViewModel model)
        {
            Doctor doctor = _mapper.Map<Doctor>(model);

            doctor.Id = Guid.NewGuid().ToString();

            var account = await _userService.CreateFromViewAsync(new UserViewModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserEmail = model.Email,
                Password = string.Format($"{model.FirstName.ToLower()}{model.LastName}{model.Age}@"),
                Role = Role.Moderator
            }, 
            Role.Moderator
            );

            doctor.UserAccountId = account.Id;
            
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
                throw new KeyNotFoundException(nameof(id));
            }
            return _mapper.Map<DoctorViewModel>(doctor);
        }

        public async Task<DoctorViewModel> GetByUserAccountIdAsync(string id)
        {
            Doctor? doctor = await _repository.GetByUserAccountIdAsync(id);

            if (doctor is null)
            {
                throw new KeyNotFoundException(nameof(id));
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
