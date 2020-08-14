using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Restaurant.Database.Services;
using Restaurant.Services.Models.Item;
using Restaurant.Database.Services.Implementations;

namespace Restaurant.Services.Implementations
{
    public class ItemService : IItemService
    {
        #region Declarations

        private readonly IItemDbService itemDb;

        #endregion

        #region Constructors

        public ItemService()
        {
            itemDb = new ItemDbService();
        }

        #endregion

        #region Methods

        public IEnumerable<ItemDto> GetAllItems()
        {
            return itemDb.GetAllItems().Select(i => new ItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                ImageSource = ConvertFromByteArrayToImageSource(i.Image.Content)
            }).ToList();
        }

        public void CreateItem(string name, decimal price, byte[] imageContent)
        {
            itemDb.CreateItem(name, price, imageContent);
        }

        private static ImageSource ConvertFromByteArrayToImageSource(byte[] imageContent)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageContent);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            return biImg;
        }

        #endregion
    }
}
