using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Person : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [NotMapped]
        public int Age { get; set; }

        [NotMapped]
        public string Gender { get; set; }

        [NotMapped]
        public string Address { get; set; }

        [NotMapped]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
