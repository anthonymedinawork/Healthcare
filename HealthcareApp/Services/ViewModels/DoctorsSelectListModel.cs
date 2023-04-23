using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthcareApp.Services.ViewModels
{
    public class DoctorsSelectListModel
    {
        public string SelectedDoctor { get; set; }
        public List<SelectListItem> DoctorsSelectList { get; set; }
    }
}
