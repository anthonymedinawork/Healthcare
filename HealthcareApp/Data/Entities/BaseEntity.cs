using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }
    }
}
