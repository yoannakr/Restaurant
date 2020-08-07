using System.ComponentModel.DataAnnotations;

namespace Restaurant.Database.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string Source { get; set; }

        public Item Item { get; set; }
    }
}
