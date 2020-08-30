using System.Linq;
using Prism.Commands;
using Restaurant.Database.Models;
using System.Collections.ObjectModel;
using Restaurant.Services.Models.Item;
using Restaurant.Common.InstanceHolder;

namespace Restaurant.ViewModels
{
    public class AllItemsViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> addItemToSelectedCommand;
        private ObservableCollection<ItemDto> items;

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

        public ObservableCollection<ItemDto> Items
        {
            get
            {
                if (items == null)
                    items = CollectionInstance.Instance.Items;

                return items;
            }
        }

        public SalesViewModel SalesViewModel { get; set; }

        public SelectedItemViewModel SelectedItemViewModel { get; set; }

        #endregion

        #region Methods

        private void AddItemToSelected(object obj)
        {
            if (!SalesViewModel.TableViewModel.Table.IsTaken)
                SalesViewModel.TakeTableCommand.Execute();

            ItemDto itemDto = obj as ItemDto;

            RowItemViewModel selectedItem = SelectedItemViewModel.SelectedItem;

            if (selectedItem != null)
            {
                if (selectedItem.Item.Id == itemDto.Id)
                {
                    selectedItem.Count += 1;

                    return;
                }

                RowItemViewModel sameExtra = SelectedItemViewModel
                                             .SelectedItem
                                             .Extras
                                             .Where(e => e.Item.Id == itemDto.Id)
                                             .FirstOrDefault();

                if (sameExtra != null)
                {
                    sameExtra.Count += 1;

                    return;
                }

                RowItemViewModel extra = new RowItemViewModel(SelectedItemViewModel)
                {
                    Item = new Item()
                    {
                        Id = itemDto.Id,
                        Name = itemDto.Name,
                        Price = itemDto.Price
                    },
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
                    Item = new Item()
                    {
                        Id = itemDto.Id,
                        Name = itemDto.Name,
                        Price = itemDto.Price
                    },
                    Count = 1,
                    Total = itemDto.Price
                };

                SelectedItemViewModel.Items.Add(item);
            }
        }

        #endregion
    }
}
