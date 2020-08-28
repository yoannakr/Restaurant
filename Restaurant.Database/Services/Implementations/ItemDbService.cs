using System.Linq;
using Restaurant.Database.Data;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services.Implementations
{
    public class ItemDbService : IItemDbService
    {
        #region Declarations

        private readonly RestaurantDbContext context;

        #endregion

        #region Constructors

        public ItemDbService()
        {
            context = new RestaurantDbContext();
        }

        #endregion

        #region Methods

        public IQueryable<Item> GetAllItems()
        {
            return context.Items;
        }

        public Item CreateItem(string name, decimal price, byte[] imageContent)
        {
            Image image = new Image()
            {
                Content = imageContent
            };

            context.Images.Add(image);

            Item item = new Item()
            {
                Name = name,
                Price = price,
                Image = image
            };

            context.Items.Add(item);

            context.SaveChanges();

            return item;
        }

        #endregion
    }
}
