using Data.Models;
using HealthcareApp.Services.Interfaces;
using HealthcareApp.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PatientViewModel> patients = await _service.GetAllAsync();

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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientViewModel patient)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(patient);

                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var patient = await _service.GetByIdAsync(Id);

            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PatientViewModel patient)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(patient);

                return RedirectToAction(nameof(Index));
            }
            return View(patient);
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
