using System.Linq;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services
{
    public interface IItemDbService
    {
        Item CreateItem(string name, decimal price, byte[] imageContent);

        Item UpdateItem(Item item);

        void DeleteItem(Item item);

        IQueryable<Item> GetAllItems();
    }
}
