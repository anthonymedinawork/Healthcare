using AutoMapper;
using Data.Models;
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

        public AttendanceService(IAttendanceRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task CreateAsync(AttendanceViewModel model)
        {
            Attendance attendance = _mapper.Map<Attendance>(model);

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

        public async Task<List<AttendanceViewModel>> GetAllAsync()
        {
            var attendances = await _repository.GetAll().ToListAsync();

            return _mapper.Map<List<AttendanceViewModel>>(attendances);
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
                throw new ArgumentNullException("No such attendance exists!");
            }
            return _mapper.Map<AttendanceViewModel>(attendance);
        }

        public async Task UpdateAsync(AttendanceViewModel model)
        {
            Attendance attendance = _mapper.Map<Attendance>(model);

            await _repository.UpdateAsync(attendance);
        }
    }
}
