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
        private List<ItemWithImageSourceServiceModel> items;
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

        public List<ItemWithImageSourceServiceModel> Items
        {
            get
            {
                if (items == null)
                    items = new List<ItemWithImageSourceServiceModel>();

                items = itemService.GetAllItems().ToList();

                return items;

            }
        }

        #endregion

        #region Methods

        private void AddItem(object obj)
        {
            // make id,search by id
            string name = obj as string;

            //this.salesViewModel.SelectedItemViewModel.Items.Add(item);
        }

        #endregion
    }
}
