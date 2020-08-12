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

        #endregion

        #region Constructor

        public AllItemViewModel(SalesViewModel salesViewModel)
        {
            itemService = new ItemService();
            this.salesViewModel = salesViewModel;
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

            RowItemViewModel rowItemViewModel = new RowItemViewModel()
            {
                Item = new Item()
                {
                    Name = itemDto.Name,
                    Price = itemDto.Price
                },
                Count = 1,
                Total = itemDto.Price
            };

            rowItemViewModel.Extras.Add(rowItemViewModel);

            this.salesViewModel.SelectedItemViewModel.Items.Add(rowItemViewModel);
        }

        #endregion
    }
}
