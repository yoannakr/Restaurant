using System.Linq;
using Restaurant.Common.Helpers;
using System.Collections.Generic;
using Restaurant.Database.Models;
using Restaurant.Database.Services;
using Restaurant.Services.Models.Item;
using Restaurant.Common.InstanceHolder;
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
                ImageContent = i.Image.Content,
                ImageSource = ImageHelper.ConvertFromByteArrayToImageSource(i.Image.Content)
            }).ToList();
        }

        public Item CreateItem(string name, decimal price, byte[] imageContent)
        {
            Item item = itemDb.CreateItem(name, price, imageContent);

            CollectionInstance.Instance.Items.Add(new ItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                ImageContent = item.Image.Content,
                ImageSource = ImageHelper.ConvertFromByteArrayToImageSource(item.Image.Content)
            });

            return item;
        }

        public Item UpdateItem(Item item)
        {
            return itemDb.UpdateItem(item);
        }

        public void DeleteItem(Item item)
        {
            itemDb.DeleteItem(item);
        }

        #endregion
    }
}
