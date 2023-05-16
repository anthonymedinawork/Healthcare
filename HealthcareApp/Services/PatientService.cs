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
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;
        private readonly IUserService _userService;
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository repository, IMapper mapper, IUserService userService, IDoctorService doctorService)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._userService = userService;
            this._doctorService = doctorService;
        }

        public async Task CreateAsync(PatientViewModel model)
        {
            Patient patient = _mapper.Map<Patient>(model);

            patient.Id = Guid.NewGuid().ToString();

            var account = await _userService.CreateFromViewAsync(new UserViewModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserEmail = model.Email,
                Password = string.Format($"{model.FirstName.ToLower()}{model.LastName}{model.Age}@"),
                Role = Role.User
            }, 
            Role.User
            );

            patient.UserAccountId = account.Id;

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

        public async Task<List<PatientViewModel>> GetAllAsync(string requesterName)
        {
            var user = await _userService.GetByUserNameAsync(requesterName);

            List<Patient> patients = new();

            if (user.Role == Role.Admin)
            {
                patients = await _repository.GetAll().ToListAsync();
            }

            else if (user.Role == Role.Moderator)
            {
                var doctorPatientsToView = await _doctorService.GetByUserAccountIdAsync(user.Id);

                patients = await _repository.GetAll().Where(p => p.PersonalDoctorId == doctorPatientsToView.Id).ToListAsync();
            }

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
                throw new KeyNotFoundException(nameof(id));
            }
            return _mapper.Map<PatientViewModel>(patient);
        }

        public async Task<PatientViewModel> GetByUserAccountIdAsync(string id)
        {
            Patient? patient = await _repository.GetByUserAccountIdAsync(id);

            if (patient is null)
            {
                throw new KeyNotFoundException(nameof(id));
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
