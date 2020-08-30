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

        #region Constructors

        public SelectedItemViewModel(TableViewModel tableViewModel)
        {
            TableViewModel = tableViewModel;
        }

        #endregion

        #region Properties

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
                    finishPaymentCommand = new DelegateCommand<object>(FinishPayment, CanFinishPayment);

                return finishPaymentCommand;
            }
        }

        public ObservableCollection<RowItemViewModel> Items
        {
            get
            {
                if (items == null)
                    items = new ObservableCollection<RowItemViewModel>();

                return items;
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

        public TableViewModel TableViewModel { get; set; }

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

            foreach (RowItemViewModel item in Items)
            {
                stringBuilder.AppendLine($"{item.Item.Name}   {item.Count}x{item.Item.Price}      {item.Total}");
                foreach (RowItemViewModel extra in item.Extras)
                {
                    stringBuilder.AppendLine($" {extra.Item.Name}   {extra.Count}x{extra.Item.Price}      {extra.Total}");
                }
            }

            stringBuilder.AppendLine("~~~~~~~~~~~~");
            stringBuilder.AppendLine($"Обща сума : {Total}");
            stringBuilder.AppendLine($"Платени : {Payed}");
            stringBuilder.AppendLine($"Ресто : {Payed - Total}");
            stringBuilder.AppendLine("Искате ли да приключите сметката?");

            if (MessageBox.Show(stringBuilder.ToString(),
                    "Потвърждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Items.Clear();
                Total = 0;
                Payed = 0;
                TableViewModel.IsTaken = false;
                MenuViewModel.Instance.ChangeMenuViewCommand.Execute(MenuViewModel.Instance.AdminPanelViewModel);
            }

        }

        private bool CanFinishPayment(object arg)
        {
            return IsValid();
        }

        private bool IsValid()
        {
            if (Payed <= 0 || Total == 0 || Payed < Total)
                return false;

            return true;
        }

        #endregion
    }
}
