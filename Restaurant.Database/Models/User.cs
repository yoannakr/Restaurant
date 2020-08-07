using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Restaurant.Database.Models.DataValidation.User;

namespace Restaurant.Database.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<UserRole> Roles { get; set; } = new HashSet<UserRole>();
    }
}
