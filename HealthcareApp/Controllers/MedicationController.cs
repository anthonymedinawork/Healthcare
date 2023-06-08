using HealthcareApp.Services.Interfaces;
using HealthcareApp.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApp.Controllers
{
    public class MedicationController : Controller
    {
        private readonly IMedicationService _service;

        public MedicationController(IMedicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            List<MedicationViewModel> meds = await _service.GetAllAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                meds = meds.Where(s => s.Indication.ToLower().Contains(searchString)).ToList();
            }

            return View(meds);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                MedicationViewModel med = await _service.GetByIdAsync(id);

                return View(med);
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

        public ActionResult ShowSuccessMessage()
        {
            return PartialView(@"~/Views/Shared/_SuccessPartial.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> LoadDb()
        {
            try
            {
                await _service.LoadDbAsync();

                return RedirectToAction(nameof(ShowSuccessMessage));
            }
            catch (ArgumentException e)
            {
                ViewData["Message"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTableData()
        {
            try
            {
                await _service.DeleteTableDataAsync();

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
