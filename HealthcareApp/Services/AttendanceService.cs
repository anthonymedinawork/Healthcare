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
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IMedicationService _medicationService;

        public AttendanceService(IAttendanceRepository repository, IMapper mapper, IUserService userService, 
            IDoctorService doctorService, IPatientService patientService, IMedicationService medService)
        {
            _repository = repository;
            _mapper = mapper;
            _userService = userService;
            _doctorService = doctorService;
            _patientService = patientService;
            _medicationService = medService;
        }

        public async Task CreateAsync(string requesterName, AttendanceViewModel model)
        {
            var user = await _userService.GetByUserNameAsync(requesterName);

            var doctor = await _doctorService.GetByUserAccountIdAsync(user.Id);

            model.DoctorId = doctor.Id;

            Attendance attendance = _mapper.Map<Attendance>(model);

            attendance.Id = Guid.NewGuid().ToString();
            attendance.Medication = null;

            await _repository.CreateAsync(attendance);
        }

        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new KeyNotFoundException(nameof(id));
            }
            await _repository.DeleteAsync(id);
        }

        public async Task<List<AttendanceViewModel>> GetAllAsync(string requesterName)
        {
            var user = await _userService.GetByUserNameAsync(requesterName);

            List<Attendance> attendances = new();

            if (user.Role == Role.User)
            {
                var patientAttendancesToView = await _patientService.GetByUserAccountIdAsync(user.Id);

                attendances = await _repository.GetAll().Where(a => a.PatientId == patientAttendancesToView.Id).ToListAsync();
            }
            else if (user.Role == Role.Moderator)
            {
                var doctorAttendancesToView = await _doctorService.GetByUserAccountIdAsync(user.Id);

                attendances = await _repository.GetAll().Where(a => a.DoctorId == doctorAttendancesToView.Id).ToListAsync();
            }
            else
            {
                attendances = await _repository.GetAll().ToListAsync();
            }

            var mapAttendances = _mapper.Map<List<AttendanceViewModel>>(attendances);

            foreach (var attendance in mapAttendances)
            {
                var doctor = await _doctorService.GetByIdAsync(attendance.DoctorId);
                var patient = await _patientService.GetByIdAsync(attendance.PatientId);
                var medication = await _medicationService.GetByIdAsync(attendance.MedicationId);

                attendance.PatientName = patient.FirstName + " " + patient.LastName;
                attendance.DoctorName = doctor.FirstName + " " + doctor.LastName;
                attendance.MedicationName = medication.Name;
            }

            return mapAttendances;
        }

        public async Task<List<AttendanceViewModel>> GetAllAsync(Expression<Func<AttendanceViewModel, bool>> filter)
        {
            var attendanceFilter = _mapper.Map<Expression<Func<Attendance, bool>>>(filter);

            List<Attendance> attendances = await _repository.GetAll(attendanceFilter).ToListAsync();

            return _mapper.Map<List<AttendanceViewModel>>(attendances);
        }

        public async Task<AttendanceViewModel> GetByIdAsync(string id)
        {
            Attendance? attendance = await _repository.GetByIdAsync(id);

            if (attendance is null)
            {
                throw new KeyNotFoundException(nameof(id));
            }

            var mapAttendance = _mapper.Map<AttendanceViewModel>(attendance);

            var doctor = await _doctorService.GetByIdAsync(mapAttendance.DoctorId);
            var patient = await _patientService.GetByIdAsync(mapAttendance.PatientId);
            var medication = await _medicationService.GetByIdAsync(mapAttendance.MedicationId);

            mapAttendance.PatientName = patient.FirstName + " " + patient.LastName;
            mapAttendance.DoctorName = doctor.FirstName + " " + doctor.LastName;
            mapAttendance.MedicationName = medication.Name;

            return mapAttendance;
        }

        public async Task UpdateAsync(AttendanceViewModel model)
        {
            Attendance attendance = _mapper.Map<Attendance>(model);

            await _repository.UpdateAsync(attendance);
        }
    }
}
