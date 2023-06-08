using Data.Models;
using HealthcareApp.Contracts.Extensions;
using HealthcareApp.Services.Interfaces;
using HealthcareApp.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthcareApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _service;
        private readonly IDoctorService _doctorService;
        private readonly string username;

        public PatientController(IPatientService service, IDoctorService doctorService, IHttpContextAccessor http)
        {
            _service = service;
            _doctorService = doctorService;
            username = http.GetRequesterId();
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            List<PatientViewModel> patients = await _service.GetAllAsync(username);

            if (!String.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(s => s.FirstName.ToLower()!.Contains(searchString.ToLower())).ToList();
            }

            return View(patients);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                PatientViewModel patient = await _service.GetByIdAsync(id);

                return View(patient);
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
            var doctors = await _doctorService.GetAllAsync();

            var model = new PatientViewModel();
            model.DoctorsList = new DoctorsSelectListModel();
            model.DoctorsList.DoctorsSelectList = new List<SelectListItem>();

            foreach (var item in doctors)
            {
                model.DoctorsList.DoctorsSelectList.Add(new SelectListItem()
                {
                    Text = item.FirstName + " " + item.LastName,
                    Value = item.Id
                });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientViewModel patient)
        {
            try
            {
                var selectedDoctor = patient.DoctorsList.SelectedDoctor;
                patient.PersonalDoctorId = selectedDoctor;

                await _service.CreateAsync(patient);

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
        public async Task<IActionResult> Edit(string Id)
        {
            var patient = await _service.GetByIdAsync(Id);

            var doctors = await _doctorService.GetAllAsync();

            patient.DoctorsList = new DoctorsSelectListModel();
            patient.DoctorsList.SelectedDoctor = patient.PersonalDoctorId;
            patient.DoctorsList.DoctorsSelectList = new List<SelectListItem>();

            foreach (var item in doctors)
            {
                patient.DoctorsList.DoctorsSelectList.Add(new SelectListItem()
                {
                    Text = item.FirstName + " " + item.LastName,
                    Value = item.Id
                });
            }

            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientViewModel patient)
        {
            try
            {
                var selectedDoctor = patient.DoctorsList.SelectedDoctor;
                patient.PersonalDoctorId = selectedDoctor;

                await _service.UpdateAsync(patient);

                return RedirectToAction(nameof(ShowSuccessMessage));
            }
            catch (ArgumentException e)
            {
                ViewData["Message"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                PatientViewModel patient = await _service.GetByIdAsync(id);

                return View(patient);
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
