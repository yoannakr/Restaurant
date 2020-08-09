using System.Linq;
using Restaurant.ViewModels;
using System.Collections.Generic;
using Restaurant.Database.Services;
using Restaurant.Database.Services.Implementations;

namespace Restaurant.Services.Implementations
{
    public class ItemService : IItemService
    {
        #region Declarations

        private readonly IItemDbService itemDb;

        #endregion

        #region Constructor

        public ItemService()
        {
            itemDb = new ItemDbService();
        }

        #endregion

        #region Methods

        public IEnumerable<ItemViewModel> GetAllItems()
        {
            return itemDb.GetAllItems().Select(i => new ItemViewModel
            {
                Name = i.Name,
                Price = i.Price,
                Image = i.Image.Content
            }).ToList();
        }

        public void CreateItem(string name, decimal price, byte[] imageContent)
        {
            itemDb.CreateItem(name, price, imageContent);
        }

        #endregion
    }
}
