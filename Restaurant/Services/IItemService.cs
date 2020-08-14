using System.Collections.Generic;
using Restaurant.Services.Models.Item;

namespace Restaurant.Services
{
    public interface IItemService
    {
        void CreateItem(string name, decimal price, byte[] imageContent);

        IEnumerable<ItemDto> GetAllItems();
    }
}