using System;
using System.Windows;
using Prism.Commands;
using Restaurant.Services;
using Restaurant.Common.Helpers;
using Restaurant.Services.Implementations;
using System.Collections.ObjectModel;
using Restaurant.Services.Models.Category;
using Restaurant.Common.InstanceHolder;
using System.Collections.Generic;
using Restaurant.Database.Models;
using System.Linq;

namespace Restaurant.ViewModels
{
    public class CreateItemViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> addCategoryCommand;
        private DelegateCommand<object> browseCommand;
        private DelegateCommand<object> createItemCommand;
        private DelegateCommand<object> returnCommand;
        private readonly IItemService itemService;
        private ObservableCollection<CategoryDto> categories;
        private List<CategoryDto> itemCategories;
        private string name;
        private decimal price;
        private string imageSource;
        private byte[] imageContent;

        #endregion

        #region Constructors

        public CreateItemViewModel()
        {
            itemService = new ItemService();
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

        public DelegateCommand<object> CreateItemCommand
        {
            get
            {
                if (createItemCommand == null)
                    createItemCommand = new DelegateCommand<object>(CreateItem, CanCreateItem);

                return createItemCommand;
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

        public ObservableCollection<CategoryDto> Categories
        {
            get
            {
                if (categories == null)
                {
                    categories = CollectionInstance.Instance.Categories;

                    foreach (CategoryDto category in categories)
                    {
                        category.IsChecked = false;
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
                    itemCategories = new List<CategoryDto>();

                return itemCategories;
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                CreateItemCommand.RaiseCanExecuteChanged();
            }
        }

        public decimal Price
        {
            get => price;
            set
            {
                price = value;
                CreateItemCommand.RaiseCanExecuteChanged();
            }
        }

        public string ImageSource
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
                CreateItemCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Methods

        private void AddCategory(object obj)
        {
            CategoryDto category = obj as CategoryDto;

            if (category.IsChecked)
                ItemCategories.Add(category);
            else
                ItemCategories.Remove(category);

            CreateItemCommand.RaiseCanExecuteChanged();
        }

        private void BrowseFolder(object obj)
        {
            string path = BrowseFolderHelper.BrowseFolder();

            if (path == null)
            {
                MessageBox.Show("Грешка !");
                return;
            }

            ImageSource = path;
            ImageContent = System.IO.File.ReadAllBytes(path);
        }

        private void CreateItem(object obj)
        {
            List<Category> categories = ItemCategories
                                .Select(itc => new Category()
                                {
                                    Id = itc.Id,
                                    Name = itc.Name
                                }).ToList();

            try
            {
                itemService.CreateItem(Name, Price, ImageContent, categories);
            }
            catch (Exception)
            {
                MessageBox.Show("Грешка с базата ! Опитайте отново !");
                return;
            }

            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(MenuViewModel.Instance.AdminPanelViewModel);
        }

        private bool CanCreateItem(object arg)
        {
            return IsValid();
        }

        private bool IsValid()
        {
            if (string.IsNullOrEmpty(Name) || Price <= 0 || ImageContent == null || ItemCategories.Count == 0)
                return false;

            return true;
        }

        private void Return(object obj)
        {
            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(MenuViewModel.Instance.AdminPanelViewModel);
        }

        #endregion
    }
}
