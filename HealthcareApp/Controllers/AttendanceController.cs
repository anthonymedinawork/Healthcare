using Data.Models;
using HealthcareApp.Contracts.Extensions;
using HealthcareApp.Services.Interfaces;
using HealthcareApp.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthcareApp.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _service;
        private readonly IMedicationService _medicationService;
        private readonly IPatientService _patientService;
        private readonly string username;

        public AttendanceController(IAttendanceService service, IMedicationService medicationService, 
            IPatientService patientService, IHttpContextAccessor http)
        {
            _service = service;
            _medicationService = medicationService;
            _patientService = patientService;
            username = http.GetRequesterId();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AttendanceViewModel> attendances = await _service.GetAllAsync(username);

            return View(attendances);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                AttendanceViewModel attendance = await _service.GetByIdAsync(id);

                return View(attendance);
            }
            catch (ArgumentException e)
            {
                ViewData["Message"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var meds = await _medicationService.GetAllAsync();
            var patients = await _patientService.GetAllAsync();

            var model = new AttendanceViewModel();

            model.MedicationsList = new MedicationsSelectListModel();
            model.MedicationsList.MedicationsSelectList = new List<SelectListItem>();

            model.PatientsList = new PatientsSelectListModel();
            model.PatientsList.PatientsSelectList = new List<SelectListItem>();           
            
            foreach (var item in meds)
            {
                model.MedicationsList.MedicationsSelectList.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id
                });
            }

            foreach (var item in patients)
            {
                model.PatientsList.PatientsSelectList.Add(new SelectListItem()
                {
                    Text = item.FirstName + " " + item.LastName,
                    Value = item.Id
                });
            }         

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AttendanceViewModel attendance)
        {
            try
            {
                var selectedPatient = attendance.PatientsList.SelectedPatient;
                attendance.PatientId = selectedPatient;

                var selectedMedication = attendance.MedicationsList.SelectedMedication;
                attendance.MedicationId = selectedMedication;

                await _service.CreateAsync(username, attendance);

                return RedirectToAction(nameof(ShowSuccessMessage));
            }

            catch (ArgumentException e)
            {
                ViewData["Message"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult ShowSuccessMessage()
        {
            return PartialView(@"~/Views/Shared/_SuccessPartial.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                AttendanceViewModel attendance = await _service.GetByIdAsync(id);

                return View(attendance);
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

                return RedirectToAction(nameof(ShowSuccessMessage));
            }
            catch (ArgumentException e)
            {
                ViewData["Message"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
