using System.Linq;
using Prism.Commands;
using System.Collections.ObjectModel;
using Restaurant.Services.Models.Item;
using Restaurant.Common.InstanceHolder;
using Restaurant.Services.Models.Category;
using System.Windows;

namespace Restaurant.ViewModels
{
    public class AllItemsViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> addItemToSelectedCommand;
        private DelegateCommand<object> changeCategoryCommand;
        private ObservableCollection<ItemDto> items;
        private ObservableCollection<CategoryDto> categories;

        #endregion

        #region Constructors

        public AllItemsViewModel(SalesViewModel salesViewModel)
        {
            SalesViewModel = salesViewModel;
            SelectedItemViewModel = salesViewModel.SelectedItemViewModel;
        }

        #endregion

        #region Properties

        public DelegateCommand<object> AddItemToSelectedCommand
        {
            get
            {
                if (addItemToSelectedCommand == null)
                    addItemToSelectedCommand = new DelegateCommand<object>(AddItemToSelected);

                return addItemToSelectedCommand;
            }
        }

        public DelegateCommand<object> ChangeCategoryCommand
        {
            get
            {
                if (changeCategoryCommand == null)
                    changeCategoryCommand = new DelegateCommand<object>(ChangeCategory);

                return changeCategoryCommand;
            }
        }

        public ObservableCollection<ItemDto> Items
        {
            get
            {
                if (items == null)
                    items = CollectionInstance.Instance.Items;

                return items;
            }
            set
            {
                items = value;

                OnPropertyChanged("Items");
            }
        }

        public ObservableCollection<CategoryDto> Categories
        {
            get
            {
                if (categories == null)
                    categories = CollectionInstance.Instance.Categories;

                return categories;
            }
        }

        public SalesViewModel SalesViewModel { get; set; }

        public SelectedItemViewModel SelectedItemViewModel { get; set; }

        #endregion

        #region Methods

        private void AddItemToSelected(object obj)
        {
            if (!SalesViewModel.TableViewModel.TableDto.IsTaken)
                SalesViewModel.TakeTableCommand.Execute();

            ItemDto itemDto = obj as ItemDto;

            if (itemDto == null)
            {
                MessageBox.Show("Грешка !");
                return;
            }

            RowItemViewModel selectedItem = SelectedItemViewModel.SelectedItem;

            if (selectedItem != null)
            {
                if (selectedItem.ItemDto.Id == itemDto.Id)
                {
                    selectedItem.Count += 1;

                    return;
                }

                RowItemViewModel sameExtra = SelectedItemViewModel
                                             .SelectedItem
                                             .Extras
                                             .Where(e => e.ItemDto.Id == itemDto.Id)
                                             .FirstOrDefault();

                if (sameExtra != null)
                {
                    sameExtra.Count += 1;

                    return;
                }

                RowItemViewModel extra = new RowItemViewModel(SelectedItemViewModel)
                {
                    ItemDto = itemDto,
                    Price = itemDto.Price,
                    Count = 1,
                    Total = itemDto.Price,
                    RowItemViewModelex = SelectedItemViewModel.SelectedItem
                };

                SelectedItemViewModel.SelectedItem.Extras.Add(extra);

                selectedItem.ShowListBtnVisibility = true;
            }
            else
            {
                RowItemViewModel item = new RowItemViewModel(SelectedItemViewModel)
                {
                    ItemDto = itemDto,
                    Price = itemDto.Price * ((100 - SelectedItemViewModel.Discount) / 100),
                    Count = 1,
                    Total = itemDto.Price * ((100 - SelectedItemViewModel.Discount) / 100)
                };

                SelectedItemViewModel.Items.Add(item);
            }
        }

        private void ChangeCategory(object obj)
        {
            if (obj is string All)
            {
                Items = CollectionInstance.Instance.Items;
                return;
            }

            CategoryDto categoryDto = obj as CategoryDto;

            if (categoryDto == null)
            {
                MessageBox.Show("Грешка !");
                return;
            }

            Items = new ObservableCollection<ItemDto>(CollectionInstance.Instance.Items
                                                     .Where(i => i.Categories.Any(c => c.Id == categoryDto.Id)).ToList());
        }

        #endregion
    }
}
