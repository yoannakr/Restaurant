using Prism.Commands;
using Restaurant.Database.Models;
using System.Collections.ObjectModel;

namespace Restaurant.ViewModels
{
    public class RowItemViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> deleteExtraCommand;
        private decimal total;
        private int count;
        private ObservableCollection<RowItemViewModel> extras;
        private SelectedItemViewModel selectedItemViewModel;

        #endregion

        #region Constructors

        public RowItemViewModel(SelectedItemViewModel selectedItemViewModel)
        {
            this.selectedItemViewModel = selectedItemViewModel;
        }

        #endregion

        #region Properties

        public ObservableCollection<RowItemViewModel> Extras
        {
            get
            {
                if (extras == null)
                    extras = new ObservableCollection<RowItemViewModel>();

                return extras;
            }
        }

        public RowItemViewModel RowItemViewModelex { get; set; }

        public Item Item { get; set; }

        public int Count
        {
            get => count;
            set
            {
                count = value;
                selectedItemViewModel.Total -= Total;
                Total = count * Item.Price;
                selectedItemViewModel.Total += Total;
                OnPropertyChanged("Count");
            }
        }

        public decimal Total
        {
            get => total;
            set
            {
                total = value;
                OnPropertyChanged("Total");
            }
        }

        public DelegateCommand<object> DeleteExtraCommand
        {
            get
            {
                if (deleteExtraCommand == null)
                    deleteExtraCommand = new DelegateCommand<object>(DeleteExtra);

                return deleteExtraCommand;
            }
        }

        #endregion

        #region Methods

        private void DeleteExtra(object obj)
        {
            RowItemViewModel item = obj as RowItemViewModel;

            RowItemViewModelex.Extras.Remove(item);

            selectedItemViewModel.Total -= item.Total;
        }


        #endregion
    }
}
