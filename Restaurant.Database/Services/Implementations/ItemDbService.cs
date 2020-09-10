using System;
using System.Linq;
using Restaurant.Database.Data;
using Restaurant.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        public Item CreateItem(string name, decimal price,decimal discount, byte[] imageContent, List<Category> categories)
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
                Discount = discount,
                Image = image
            };

            context.Items.Add(item);

            List<ItemCategory> itemCategories = categories.Select(c => new ItemCategory()
            {
                Item = item,
                ItemId = item.Id,
                Category = c,
                CategoryId = c.Id
            }).ToList();

            foreach (ItemCategory itemCategory in itemCategories)
            {
                item.Categories.Add(itemCategory);
            }

            context.SaveChanges();

            return item;
        }

        public Item UpdateItem(Item item, List<ItemCategory> itemCategories)
        {
            Item entityItem = context.Items.Include(i => i.Image).Include(i=>i.Categories).FirstOrDefault(i => i.Id == item.Id);

            if (entityItem == null)
                throw new Exception();

            entityItem.Name = item.Name;
            entityItem.Price = item.Price;
            entityItem.Discount = item.Discount;

            Image previousImage = entityItem.Image;
            entityItem.Image = item.Image;

            List<ItemCategory> categoriesForRemoval = new List<ItemCategory>();
            foreach (ItemCategory category in entityItem.Categories)
            {
                if (itemCategories.FirstOrDefault(itc => itc.CategoryId == category.CategoryId && itc.ItemId == category.ItemId) == null)
                {
                    categoriesForRemoval.Add(category);
                }
            }

            foreach (ItemCategory category in categoriesForRemoval)
            {
                entityItem.Categories.Remove(category);
            }

            foreach (ItemCategory itemCategory in itemCategories)
            {
                if (entityItem.Categories.FirstOrDefault(c => c.CategoryId == itemCategory.CategoryId && c.ItemId == itemCategory.ItemId) == null)
                {
                    entityItem.Categories.Add(itemCategory);
                }
            }

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
