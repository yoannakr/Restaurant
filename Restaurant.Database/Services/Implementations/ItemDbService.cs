using System;
using System.Linq;
using Restaurant.Database.Data;
using Restaurant.Database.Models;
using Microsoft.EntityFrameworkCore;

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

        public Item UpdateItem(Item item)
        {
            Item entityItem = context.Items.Include(i => i.Image).FirstOrDefault(i => i.Id == item.Id);

            if (entityItem == null)
                throw new Exception();

            entityItem.Name = item.Name;
            entityItem.Price = item.Price;

            Image previousImage = entityItem.Image;
            entityItem.Image = item.Image;

            context.SaveChanges();

            context.Images.Remove(previousImage);

            context.SaveChanges();

            return entityItem;
        }

        public void DeleteItem(Item item)
        {
            Item entityItem = context.Items.Include(i => i.Image).FirstOrDefault(i => i.Id == item.Id);

            if (entityItem == null)
                throw new Exception();

            context.Items.Remove(entityItem);

            context.Images.Remove(entityItem.Image);

            context.SaveChanges();
        }

        #endregion
    }
}
