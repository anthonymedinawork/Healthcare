using AutoMapper;
using Data.Models;
using HealthcareApp.Data.Enums;
using HealthcareApp.Repositories.Interfaces;
using HealthcareApp.Services.Interfaces;
using HealthcareApp.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthcareApp.Services
{
    [Authorize]
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IUserService _userService;

        public AppointmentService(IAppointmentRepository repository, IMapper mapper, IPatientService patientService, IUserService userService, IDoctorService doctorService)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._patientService = patientService;
            this._doctorService = doctorService;
            this._userService = userService;
        }

        public async Task CreateAsync(string requesterName, AppointmentViewModel model)
        {
            var user = await _userService.GetByUserNameAsync(requesterName);

            var patient = await _patientService.GetByUserAccountIdAsync(user.Id);

            model.PatientId = patient.Id;
            model.DoctorId = patient.PersonalDoctorId;
            model.IsApproved = false;

            Appointment appointment = _mapper.Map<Appointment>(model);

            appointment.Id = Guid.NewGuid().ToString();

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

        public async Task<List<AppointmentViewModel>> GetAllAsync(string requesterName)
        {
            var user = await _userService.GetByUserNameAsync(requesterName);

            List<Appointment> appointments = new();

            if (user.Role == Role.User)
            {
                var patientAppointmentsToView = await _patientService.GetByUserAccountIdAsync(user.Id);

                appointments = await _repository.GetAll().Where(a => a.PatientId == patientAppointmentsToView.Id).ToListAsync();
            }
            else if (user.Role == Role.Moderator)
            {
                var doctorAppointmentsToView = await _doctorService.GetByUserAccountIdAsync(user.Id);

                appointments = await _repository.GetAll().Where(a => a.DoctorId == doctorAppointmentsToView.Id).ToListAsync();
            }
            else
            {
                appointments = await _repository.GetAll().ToListAsync();
            }

            var mapAppointments =  _mapper.Map<List<AppointmentViewModel>>(appointments);

            foreach (var appointment in mapAppointments)
            {
                var doctor = await _doctorService.GetByIdAsync(appointment.DoctorId);
                var patient = await _patientService.GetByIdAsync(appointment.PatientId);
                appointment.PatientName = patient.FirstName + " " + patient.LastName;
                appointment.DoctorName = doctor.FirstName + " " + doctor.LastName;
            }

            return mapAppointments;
        }

        public async Task<List<AppointmentViewModel>> GetAllAsync(Expression<Func<AppointmentViewModel, bool>> filter)
        {
            var appointmentFilter = _mapper.Map<Expression<Func<Appointment, bool>>>(filter);

            List<Appointment> appointments = await _repository.GetAll(appointmentFilter).ToListAsync();

            var mapAppointments = _mapper.Map<List<AppointmentViewModel>>(appointments);

            foreach (var appointment in mapAppointments)
            {
                var doctor = await _doctorService.GetByIdAsync(appointment.DoctorId);
                var patient = await _patientService.GetByIdAsync(appointment.PatientId);
                appointment.PatientName = patient.FirstName + " " + patient.LastName;
                appointment.DoctorName = doctor.FirstName + " " + doctor.LastName;
            }

            return mapAppointments;
        }

        public async Task<AppointmentViewModel> GetByIdAsync(string id)
        {
            Appointment? appointment = await _repository.GetByIdAsync(id);

            if (appointment is null)
            {
                throw new KeyNotFoundException(nameof(id));
            }

            var mapAppointment = _mapper.Map<AppointmentViewModel>(appointment);
            var doctor = await _doctorService.GetByIdAsync(mapAppointment.DoctorId);
            var patient = await _patientService.GetByIdAsync(mapAppointment.PatientId);
            mapAppointment.PatientName = patient.FirstName + " " + patient.LastName;
            mapAppointment.DoctorName = doctor.FirstName + " " + doctor.LastName;

            return mapAppointment;
        }

        public async Task UpdateAsync(AppointmentViewModel model)
        {
            Appointment appointment = _mapper.Map<Appointment>(model);

            await _repository.UpdateAsync(appointment);
        }
    }
}
