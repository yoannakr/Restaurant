using System.Collections.Generic;
using Restaurant.Database.Models;
using Restaurant.Services.Models.Item;

namespace Restaurant.Services
{
    public interface IItemService
    {
        ItemDto CreateItem(string name, decimal price, byte[] imageContent, List<Category> categories);

        void UpdateItem(Item item, List<ItemCategory> itemCategories);

        void DeleteItem(Item item);

        IEnumerable<ItemDto> GetAllItems();
    }
}