using System.Windows.Media;

namespace Restaurant.Services.Models.Item
{
    public class ItemDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public ImageSource ImageSource { get; set; }
    }
}
