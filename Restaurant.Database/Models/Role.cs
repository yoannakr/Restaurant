using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Restaurant.Database.Models.DataValidation.Role;

namespace Restaurant.Database.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<UserRole> Users { get; set; } = new HashSet<UserRole>();
    }
}
