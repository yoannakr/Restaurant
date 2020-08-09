using System.Linq;
using Restaurant.Services;
using System.Collections.Generic;
using Restaurant.Services.Implementations;

namespace Restaurant.ViewModels
{
    public class AllItemViewModel : BaseViewModel
    {
        #region Declarations

        private IItemService itemService;
        private List<ItemViewModel> itemViewModels;

        #endregion

        #region Constructor

        public AllItemViewModel()
        {
            itemService = new ItemService();
        }

        #endregion

        #region Properties

        public List<ItemViewModel> ItemViewModels
        {
            get
            {
                if (itemViewModels == null)
                    itemViewModels = new List<ItemViewModel>();

                itemViewModels = itemService.GetAllItems().ToList();

                return itemViewModels;

            }
        }

        #endregion
    }
}
