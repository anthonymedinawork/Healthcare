using System.ComponentModel.DataAnnotations;

namespace HealthcareApp.Services.ViewModels
{
    public class MedicationViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
