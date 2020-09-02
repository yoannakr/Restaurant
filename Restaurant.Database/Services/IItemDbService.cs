using System.Linq;
using System.Collections.Generic;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services
{
    public interface IItemDbService
    {
        Item CreateItem(string name, decimal price, byte[] imageContent, List<Category> categories);

        Item UpdateItem(Item item);

        void DeleteItem(Item item);

        IQueryable<Item> GetAllItems();
    }
}
