using AutoMapper;
using Data.Models;
using HealthcareApp.Repositories.Interfaces;
using HealthcareApp.Services.Interfaces;
using HealthcareApp.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthcareApp.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task CreateAsync(AppointmentViewModel model)
        {
            Appointment appointment = _mapper.Map<Appointment>(model);

            await _repository.CreateAsync(appointment);
        }

        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new KeyNotFoundException(nameof(id));
            }
            await _repository.DeleteAsync(id);
        }

        public async Task<List<AppointmentViewModel>> GetAllAsync()
        {
            var appointments = await _repository.GetAll().ToListAsync();
            return _mapper.Map<List<AppointmentViewModel>>(appointments);
        }

        public async Task<List<AppointmentViewModel>> GetAllAsync(Expression<Func<DoctorViewModel, bool>> filter)
        {
            var appointmentFilter = _mapper.Map<Expression<Func<Appointment, bool>>>(filter);

            List<Appointment> appointment = await _repository.GetAll(appointmentFilter).ToListAsync();

            return _mapper.Map<List<AppointmentViewModel>>(appointment);
        }

        public async Task<AppointmentViewModel> GetByIdAsync(string id)
        {
            Appointment? appointment = await _repository.GetByIdAsync(id);

            if (appointment is null)
            {
                throw new ArgumentNullException("No such appointment exists!");
            }
            return _mapper.Map<AppointmentViewModel>(appointment);
        }

        public async Task UpdateAsync(AppointmentViewModel model)
        {
            Appointment appointment = _mapper.Map<Appointment>(model);

            await _repository.UpdateAsync(appointment);
        }
    }
}
