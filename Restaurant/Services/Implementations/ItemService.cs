using System.Linq;
using Restaurant.Common.Helpers;
using System.Collections.Generic;
using Restaurant.Database.Models;
using Restaurant.Database.Services;
using Restaurant.Services.Models.Item;
using Restaurant.Common.InstanceHolder;
using Restaurant.Database.Services.Implementations;
using Restaurant.Services.Models.Category;

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
                ImageSource = ImageHelper.ConvertFromByteArrayToImageSource(i.Image.Content),
                Categories = i.Categories.Select(itc => new CategoryDto()
                {
                    Id = itc.CategoryId,
                    Name = itc.Category.Name
                }).ToList()
            }).ToList();
        }

        public ItemDto CreateItem(string name, decimal price, byte[] imageContent, List<Category> categories)
        {
            Item item = itemDb.CreateItem(name, price, imageContent, categories);

            //TODO:Delete
            CollectionInstance.Instance.Items.Add(new ItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                ImageContent = item.Image.Content,
                ImageSource = ImageHelper.ConvertFromByteArrayToImageSource(item.Image.Content),
                Categories = item.Categories.Select(itc => new CategoryDto()
                {
                    Id = itc.CategoryId,
                    Name = itc.Category.Name
                }).ToList()
            });

            return new ItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                ImageContent = item.Image.Content,
                ImageSource = ImageHelper.ConvertFromByteArrayToImageSource(item.Image.Content),
                Categories = item.Categories.Select(itc => new CategoryDto()
                {
                    Id = itc.CategoryId,
                    Name = itc.Category.Name
                }).ToList()
            };
        }

        public void UpdateItem(Item item, List<ItemCategory> itemCategories)
        {
            itemDb.UpdateItem(item,itemCategories);
        }

        public void DeleteItem(Item item)
        {
            itemDb.DeleteItem(item);
        }

        #endregion
    }
}
