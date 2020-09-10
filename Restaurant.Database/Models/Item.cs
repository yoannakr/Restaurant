using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Restaurant.Database.Models.DataValidation.Item;

namespace Restaurant.Database.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public int ImageId { get; set; }

        public Image Image { get; set; }

        public ICollection<ItemCategory> Categories { get; set; } = new HashSet<ItemCategory>();
    }
}
