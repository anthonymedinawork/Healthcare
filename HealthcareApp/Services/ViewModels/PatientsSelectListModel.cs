using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthcareApp.Services.ViewModels
{
    public class PatientsSelectListModel
    {
        public string SelectedPatient { get; set; }
        public List<SelectListItem> PatientsSelectList { get; set; }
    }
}
