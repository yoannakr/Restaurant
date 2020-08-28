using Prism.Commands;
using Restaurant.Services;
using Restaurant.Database.Models;
using Restaurant.Services.Models.Item;
using Restaurant.Common.AllCollections;
using Restaurant.Services.Implementations;

namespace Restaurant.ViewModels
{
    public class CreateItemViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> browseCommand;
        private DelegateCommand<object> createItemCommand;
        private DelegateCommand<object> returnCommand;
        private readonly IItemService itemService;
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

        private void BrowseFolder(object obj)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files (*.png) | *.png"
            };

            bool? result = openFileDlg.ShowDialog();

            if (result == true)
            {
                ImageSource = openFileDlg.FileName;
                ImageContent = System.IO.File.ReadAllBytes(openFileDlg.FileName);
            }
        }

        private void CreateItem(object obj)
        { 
            Item item = itemService.CreateItem(Name, Price, ImageContent);

            CollectionHolder.Instance.Items.Add(new ItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                ImageSource = ImageSource
            });

            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(MenuViewModel.Instance.AdminPanelViewModel);
        }

        private bool CanCreateItem(object arg)
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
