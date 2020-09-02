using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Restaurant.Database.Models.DataValidation.Category;

namespace Restaurant.Database.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<ItemCategory> Items { get; set; } = new HashSet<ItemCategory>();
    }
}
