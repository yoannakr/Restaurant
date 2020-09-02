using System.Collections.Generic;
using Restaurant.Database.Models;
using Restaurant.Services.Models.Item;

namespace Restaurant.Services
{
    public interface IItemService
    {
        Item CreateItem(string name, decimal price, byte[] imageContent, List<Category> categories);

        Item UpdateItem(Item item);

        void DeleteItem(Item item);

        IEnumerable<ItemDto> GetAllItems();
    }
}