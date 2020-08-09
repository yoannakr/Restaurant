using System.ComponentModel.DataAnnotations;

namespace Restaurant.Database.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public byte[] Content { get; set; }

        public Item Item { get; set; }
    }
}
