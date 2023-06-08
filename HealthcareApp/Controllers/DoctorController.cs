using Data.Models;
using HealthcareApp.Services.Interfaces;
using HealthcareApp.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApp.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _service;

        public DoctorController(IDoctorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            List<DoctorViewModel> doctors = await _service.GetAllAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(s => s.FirstName.ToLower()!.Contains(searchString.ToLower())).ToList();
            }

            return View(doctors);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                DoctorViewModel doctor = await _service.GetByIdAsync(id);

                return View(doctor);
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
        public async Task<IActionResult> Create(DoctorViewModel doctor)
        {
            try
            {
                await _service.CreateAsync(doctor);

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
            var doctor = await _service.GetByIdAsync(Id);

            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorViewModel doctor)
        {
            try
            {
                await _service.UpdateAsync(doctor);

                return RedirectToAction(nameof(ShowSuccessMessage));
            }
            catch (Exception e)
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
                DoctorViewModel doctor = await _service.GetByIdAsync(id);

                return View(doctor);
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
