using System.Linq;
using System.Text;
using Prism.Commands;
using System.Windows;
using System.Collections.ObjectModel;

namespace Restaurant.ViewModels
{
    public class SelectedItemViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> deleteItemCommand;
        private DelegateCommand<object> finishPaymentCommand;
        private ObservableCollection<RowItemViewModel> items;
        private decimal total;
        private decimal payed;

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
            private set
            {
                items = value;
            }
        }

        public RowItemViewModel SelectedItem { get; set; }

        public decimal Total
        {
            get => total;
            set
            {
                total = value;

                FinishPaymentCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Total");
            }
        }

        public decimal Payed 
        {
            get => payed;
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                payed = value;
                FinishPaymentCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Payed");
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

        public DelegateCommand<object> FinishPaymentCommand
        {
            get
            {
                if (finishPaymentCommand == null)
                    finishPaymentCommand = new DelegateCommand<object>(FinishPayment,CanFinishPayment);

                return finishPaymentCommand;
            }
        }

        #endregion

        #region Methods

        private void DeleteItem(object obj)
        {
            RowItemViewModel item = obj as RowItemViewModel;

            decimal extraTotals = item.Extras.Select(e => e.Total).Sum();

            Items.Remove(item);

            Total -= item.Total;
            Total -= extraTotals;
        }

        private void FinishPayment(object obj)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Payed : {Payed}");
            stringBuilder.AppendLine($"Total : {Total}");
            stringBuilder.AppendLine($"Change : {Payed-Total}");
            stringBuilder.AppendLine("Do you want to finish?");

            if (MessageBox.Show(stringBuilder.ToString(),
                    "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Items.Clear();
                Total = 0;
                Payed = 0;
            }

        }

        private bool CanFinishPayment(object arg)
        {
            return IsValid();
        }

        private bool IsValid()
        {
            if (Payed <= 0 || Total == 0)
                return false;

            return true;
        }

        #endregion
    }
}
