using Restaurant.Services.Models.Category;
using System.Collections.Generic;
using System.Windows.Media;

namespace Restaurant.Services.Models.Item
{
    public class ItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public byte[] ImageContent { get; set; }

        public ImageSource ImageSource { get; set; }

        public ICollection<CategoryDto> Categories { get; set; }
    }
}
