using Restaurant.Services.Models.Item;
using System.Collections.ObjectModel;

namespace Restaurant.ViewModels
{
    public class SelectedItemViewModel : BaseViewModel
    {
        #region Declarations

        private ObservableCollection<RowItemViewModel> items;

        #endregion

        #region Properties

        public ObservableCollection<RowItemViewModel> Items
        {
            get
            {
                if (items == null)
                    items = new ObservableCollection<RowItemViewModel>();

                return items;
            }
        }

        #endregion
    }
}
