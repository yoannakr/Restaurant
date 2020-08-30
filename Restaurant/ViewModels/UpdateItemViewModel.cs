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

namespace Restaurant.ViewModels
{
    public class UpdateItemViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> browseCommand;
        private DelegateCommand<object> updateItemCommand;
        private DelegateCommand<object> returnCommand;
        private readonly IItemService itemService;
        private string name;
        private decimal price;
        private ImageSource imageSource;
        private byte[] imageContent;

        #endregion

        #region Constructors

        public UpdateItemViewModel(ItemDto item)
        {
            itemService = new ItemService();
            Item = item;
            Name = item.Name;
            Price = item.Price;
            ImageSource = item.ImageSource;
            ImageContent = item.ImageContent;
        }

        #endregion

        #region Properties

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

        public decimal Price
        {
            get => price;
            set
            {
                price = value;
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

        private void UpdateItem(object obj)
        {
            try
            {
                Item updatedItem = new Item()
                {
                    Id = Item.Id,
                    Name = Name,
                    Price = Price,
                    Image = new Image()
                    {
                        Content = ImageContent
                    }
                };

                itemService.UpdateItem(updatedItem);
            }
            catch (Exception)
            {
                MessageBox.Show("Грешка с базата ! Опитайте отново !");
                return;
            }

            Item.Name = Name;
            Item.Price = Price;
            Item.ImageSource = ImageSource;
            Item.ImageContent = ImageContent;

            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(MenuViewModel.Instance.AdminPanelViewModel);
        }

        private bool CanUpdateItem(object arg)
        {
            return IsValid();
        }

        private bool IsValid()
        {
            if (string.IsNullOrEmpty(Name) || Price <= 0 || ImageContent == null)
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
