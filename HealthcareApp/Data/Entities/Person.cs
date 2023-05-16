using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Person : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
