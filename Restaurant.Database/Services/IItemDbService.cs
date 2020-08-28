using System.Linq;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services
{
    public interface IItemDbService
    {
        Item CreateItem(string name, decimal price, byte[] imageContent);

        IQueryable<Item> GetAllItems();
    }
}
