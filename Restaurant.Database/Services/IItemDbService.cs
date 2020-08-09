using System.Linq;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services
{
    public interface IItemDbService
    {
        void CreateItem(string name, decimal price, byte[] imageContent);

        IQueryable<Item> GetAllItems();
    }
}
