using Data.Models;
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

        public AttendanceController(IAttendanceService service, IMedicationService medicationService, IPatientService patientService)
        {
            _service = service;
            _medicationService = medicationService;
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AttendanceViewModel> attendances = await _service.GetAllAsync();

            return View(attendances);
        }

        [HttpGet]
        public async Task<IActionResult> Create(string searchString)
        {
            var meds = await _medicationService.GetAllAsync();
            var patients = await _patientService.GetAllAsync();

            var model = new AttendanceViewModel();

            model.MedicationsList = new MedicationsSelectListModel();
            model.MedicationsList.MedicationsSelectList = new List<SelectListItem>();

            model.PatientsList = new PatientsSelectListModel();
            model.PatientsList.PatientsSelectList = new List<SelectListItem>();

            if (!String.IsNullOrEmpty(searchString))
            {
                meds = meds.Where(s => s.Indication.ToLower().Contains(searchString)).ToList();
            }
            
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
            await _service.CreateAsync(attendance);

            return RedirectToAction(nameof(Index));
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
