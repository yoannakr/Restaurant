using Restaurant.ViewModels;
using System.Collections.Generic;

namespace Restaurant.Services
{
    public interface IItemService
    {
        void CreateItem(string name, decimal price, byte[] imageContent);

        IEnumerable<ItemViewModel> GetAllItems();
    }
}
