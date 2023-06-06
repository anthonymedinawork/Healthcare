using HealthcareApp.Services.Interfaces;
using HealthcareApp.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HealthcareApp.Contracts.Extensions;

namespace HealthcareApp.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _service;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientervice;
        private readonly string username;

        public AppointmentController(IAppointmentService service, IDoctorService doctorService, 
            IPatientService patientService, IHttpContextAccessor http)
        {
            _service = service;
            _doctorService = doctorService;
            _patientervice = patientService;
            username = http.GetRequesterId();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AppointmentViewModel> appointments = await _service.GetAllAsync(username);

            return View(appointments);
        }

        [HttpGet]
        public async Task<IActionResult> Approve(string id)
        {
            AppointmentViewModel appointment = await _service.GetByIdAsync(id);

            appointment.IsApproved = true;

            await _service.UpdateAsync(new AppointmentViewModel()
            {
                Id = appointment.Id,
                Date = DateTime.Now,
                PatientId = appointment.PatientId,
                PatientName = appointment.PatientName,
                DoctorId = appointment.DoctorId,
                DoctorName = appointment.DoctorName,
                IsApproved = appointment.IsApproved
            });

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                AppointmentViewModel appointment = await _service.GetByIdAsync(id);

                return View(appointment);
            }
            catch (ArgumentException e)
            {
                ViewData["Message"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentViewModel appointment)
        {
            await _service.CreateAsync(username, appointment);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                AppointmentViewModel appointment = await _service.GetByIdAsync(id);

                return View(appointment);
            }
            catch (ArgumentException e)
            {
                ViewData["Message"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmation(string id)
        {
            try
            {
                await _service.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException e)
            {
                ViewData["Message"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
