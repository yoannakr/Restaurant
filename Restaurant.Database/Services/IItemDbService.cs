using System.Linq;
using System.Collections.Generic;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services
{
    public interface IItemDbService
    {
        Item CreateItem(string name, decimal price, decimal discount, byte[] imageContent, List<Category> categories);

        Item UpdateItem(Item item, List<ItemCategory> itemCategories);

        void DeleteItem(Item item);

        IQueryable<Item> GetAllItems();
    }
}
