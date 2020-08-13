using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Restaurant.ViewModels
{
    public class SelectedItemViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> deleteItemCommand;
        private ObservableCollection<RowItemViewModel> items;
        private decimal total;

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

        public RowItemViewModel SelectedItem { get; set; }

        public decimal Total 
        {
            get => total;
            set
            {
                total = value;
                OnPropertyChanged("Total");
            }
        }

        #endregion

        #region Methods

        private void DeleteItem(object obj)
        {
            RowItemViewModel item = obj as RowItemViewModel;

            Items.Remove(item);

            Total -= item.Total;
        }


        #endregion
    }
}
