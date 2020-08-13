using System.Linq;
using Prism.Commands;
using Restaurant.Services;
using System.Collections.Generic;
using Restaurant.Services.Models.Item;
using Restaurant.Services.Implementations;
using System.Windows;
using Restaurant.Database.Models;

namespace Restaurant.ViewModels
{
    public class AllItemViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> addItemToSelected;
        private readonly IItemService itemService;
        private List<ItemDto> items;
        private SalesViewModel salesViewModel;
        private SelectedItemViewModel selectedItemViewModel;

        #endregion

        #region Constructor

        public AllItemViewModel(SalesViewModel salesViewModel)
        {
            itemService = new ItemService();
            this.salesViewModel = salesViewModel;
            this.selectedItemViewModel = salesViewModel.SelectedItemViewModel;
        }

        #endregion

        #region Properties

        public DelegateCommand<object> AddItemToSelected
        {
            get
            {
                if (addItemToSelected == null)
                    addItemToSelected = new DelegateCommand<object>(AddItem);

                return addItemToSelected;
            }
        }

        public List<ItemDto> Items
        {
            get
            {
                if (items == null)
                    items = new List<ItemDto>();

                items = itemService.GetAllItems().ToList();

                return items;

            }
        }

        #endregion

        #region Methods

        private void AddItem(object obj)
        {
            ItemDto itemDto = obj as ItemDto;

            RowItemViewModel selectedItem = selectedItemViewModel.SelectedItem;

            if (selectedItem != null)
            {
                if(selectedItem.Item.Id == itemDto.Id)
                {
                    selectedItem.Count += 1;

                    return;
                }

                RowItemViewModel extra = new RowItemViewModel(selectedItemViewModel)
                {
                    Item = new Item()
                    {
                        Name = itemDto.Name,
                        Price = itemDto.Price
                    },
                    Count = 1,
                    Total = itemDto.Price,
                    RowItemViewModelex = selectedItemViewModel.SelectedItem
                };

                selectedItemViewModel.SelectedItem.Extras.Add(extra);
            }
            else
            {
                RowItemViewModel rowItemViewModel = new RowItemViewModel(selectedItemViewModel)
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

                selectedItemViewModel.Items.Add(rowItemViewModel);
            }

        }

        #endregion
    }
}
