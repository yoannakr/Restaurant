using System;
using System.IO;
using Prism.Commands;
using System.Windows;
using Restaurant.Services;
using System.Windows.Media;
using Restaurant.Common.Helpers;
using Restaurant.Database.Models;
using Restaurant.Services.Models.Item;
using Restaurant.Services.Implementations;
using System.Collections.ObjectModel;
using Restaurant.Services.Models.Category;
using Restaurant.Common.InstanceHolder;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.ViewModels
{
    public class UpdateItemViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> addCategoryCommand;
        private DelegateCommand<object> browseCommand;
        private DelegateCommand<object> updateItemCommand;
        private DelegateCommand<object> returnCommand;
        private readonly IItemService itemService;
        private string name;
        private decimal basePrice;
        private decimal price;
        private decimal discount;
        private ImageSource imageSource;
        private byte[] imageContent;
        private ObservableCollection<CategoryDto> categories;
        private List<CategoryDto> itemCategories;


        #endregion

        #region Constructors

        public UpdateItemViewModel(ItemDto item)
        {
            itemService = new ItemService();
            Item = item;
            Name = item.Name;
            BasePrice = item.BasePrice;
            Discount = item.Discount;
            ImageSource = item.ImageSource;
            ImageContent = item.ImageContent;
        }

        #endregion

        #region Properties

        public DelegateCommand<object> AddCategoryCommand
        {
            get
            {
                if (addCategoryCommand == null)
                    addCategoryCommand = new DelegateCommand<object>(AddCategory);

                return addCategoryCommand;
            }
        }

        public DelegateCommand<object> UpdateItemCommand
        {
            get
            {
                if (updateItemCommand == null)
                    updateItemCommand = new DelegateCommand<object>(UpdateItem, CanUpdateItem);

                return updateItemCommand;
            }
        }

        public DelegateCommand<object> BrowseCommand
        {
            get
            {
                if (browseCommand == null)
                    browseCommand = new DelegateCommand<object>(BrowseFolder);

                return browseCommand;
            }
        }

        public DelegateCommand<object> ReturnCommand
        {
            get
            {
                if (returnCommand == null)
                    returnCommand = new DelegateCommand<object>(Return);

                return returnCommand;
            }
        }

        public ItemDto Item { get; set; }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                UpdateItemCommand.RaiseCanExecuteChanged();
            }
        }

        public decimal BasePrice
        {
            get => Math.Round(basePrice, 2);
            set
            {
                basePrice = Math.Round(value, 2);

                Price = basePrice * ((100 - Discount) / 100);
                UpdateItemCommand.RaiseCanExecuteChanged();
            }
        }

        public decimal Price
        {
            get => Math.Round(price, 2);
            set
            {
                price = Math.Round(basePrice * ((100 - Discount) / 100), 2);
                OnPropertyChanged("Price");
            }
        }

        public decimal Discount
        {
            get => discount;
            set
            {
                discount = value;
                Price = basePrice * ((100 - Discount) / 100);
                UpdateItemCommand.RaiseCanExecuteChanged();
            }
        }

        public ImageSource ImageSource
        {
            get => imageSource;
            set
            {
                imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        public byte[] ImageContent
        {
            get => imageContent;
            set
            {
                imageContent = value;
                UpdateItemCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<CategoryDto> Categories
        {
            get
            {
                if (categories == null)
                {
                    categories = CollectionInstance.Instance.Categories;

                    foreach (CategoryDto category in categories)
                    {
                        CategoryDto categoryDto = ItemCategories.Where(c => c.Id == category.Id)
                                                   .FirstOrDefault();
                        if (categoryDto == null)
                            category.IsChecked = false;
                        else
                            category.IsChecked = true;
                    }

                }

                return categories;
            }
        }

        public List<CategoryDto> ItemCategories
        {
            get
            {
                if (itemCategories == null)
                    itemCategories = Item.Categories.ToList();

                return itemCategories;
            }
        }

        #endregion

        #region Methods

        private void BrowseFolder(object obj)
        {
            string path = BrowseFolderHelper.BrowseFolder();

            if (path == null)
            {
                MessageBox.Show("Грешка !");
                return;
            }

            ImageContent = File.ReadAllBytes(path);
            ImageSource = ImageHelper.ConvertFromByteArrayToImageSource(ImageContent);
        }

        private void AddCategory(object obj)
        {
            CategoryDto category = obj as CategoryDto;

            if (category.IsChecked)
            {
                CategoryDto insertCategory = ItemCategories.FirstOrDefault(c => c.Id == category.Id);

                if (insertCategory == null)
                    ItemCategories.Add(category);
            }
            else
            {
                CategoryDto removeCategory = ItemCategories.FirstOrDefault(c => c.Id == category.Id);
                ItemCategories.Remove(removeCategory);
            }

            UpdateItemCommand.RaiseCanExecuteChanged();
        }

        private void UpdateItem(object obj)
        {
            List<Category> categories = ItemCategories
                                    .Select(c => new Category()
                                    {
                                        Id = c.Id,
                                        Name = c.Name
                                    })
                                    .ToList();

            List<ItemCategory> itemCategories = categories.Select(c => new ItemCategory()
            {
                ItemId = Item.Id,
                CategoryId = c.Id,
                Category = c
            }).ToList();

            try
            {
                Item updatedItem = new Item()
                {
                    Id = Item.Id,
                    Name = Name,
                    Price = BasePrice,
                    Discount = Discount,
                    Image = new Image()
                    {
                        Content = ImageContent
                    }
                };

                itemService.UpdateItem(updatedItem, itemCategories);
            }
            catch (Exception)
            {
                MessageBox.Show("Грешка с базата ! Опитайте отново !");
                return;
            }

            Item.Name = Name;
            Item.Discount = Discount;
            Item.BasePrice = BasePrice;
            Item.Price = Item.BasePrice * ((100 - Discount) / 100);
            Item.ImageSource = ImageSource;
            Item.ImageContent = ImageContent;
            Item.Categories = ItemCategories;

            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(MenuViewModel.Instance.AdminPanelViewModel);
        }

        private bool CanUpdateItem(object arg)
        {
            return IsValid();
        }

        private bool IsValid()
        {
            if (string.IsNullOrEmpty(Name) || BasePrice <= 0 || Discount < 0 || ImageContent == null || ItemCategories.Count == 0)
                return false;

            return true;
        }

        private void Return(object obj)
        {
            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(MenuViewModel.Instance.AdminPanelViewModel.UpdateOrDeleteItemViewModel);
        }

        #endregion
    }
}
