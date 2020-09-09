using Restaurant.Services.Models.Category;
using System.Collections.Generic;
using System.Windows.Media;

namespace Restaurant.Services.Models.Item
{
    public class ItemDto
    {
        #region Declarations

        private decimal price;

        #endregion

        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal BasePrice { get; set; }

        public decimal Discount { get; set; }

        public decimal Price
        {
            get
            {
                if (price == 0)
                    price = BasePrice * ((100 - Discount) / 100);

                return price;
            }
            set
            {
                price = value;
            }
        }

        public byte[] ImageContent { get; set; }

        public ImageSource ImageSource { get; set; }

        public ICollection<CategoryDto> Categories { get; set; }

        #endregion
    }
}
