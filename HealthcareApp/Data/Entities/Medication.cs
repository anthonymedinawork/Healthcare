using Data.Models;
using System.ComponentModel.DataAnnotations;

namespace HealthcareApp.Data.Entities
{
    public class Medication : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string BrandName { get; set; }

        [Required]
        public string Indication { get; set; }
    }
}
