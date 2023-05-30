using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthcareApp.Services.ViewModels
{
    public class MedicationsSelectListModel
    {
        public string SelectedMedication { get; set; }
        public List<SelectListItem> MedicationsSelectList { get; set; }
    }
}
