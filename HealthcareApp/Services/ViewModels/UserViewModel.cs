using HealthcareApp.Data.Enums;

namespace HealthcareApp.Services.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserEmail { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
