using Prism.Commands;
using System;
using System.Collections.ObjectModel;

namespace Restaurant.ViewModels
{
    public class SelectedItemViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> deleteItemCommand;
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

        public DelegateCommand<object> DeleteItemCommand
        {
            get
            {
                if (deleteItemCommand == null)
                    deleteItemCommand = new DelegateCommand<object>(DeleteItem);

                return deleteItemCommand;
            }
        }

        #endregion

        #region Methods

        private void DeleteItem(object obj)
        {
            RowItemViewModel item = obj as RowItemViewModel;

            Items.Remove(item);
        }


        #endregion
    }
}
