using Restaurant.Database.Models;
using System.Collections.ObjectModel;

namespace Restaurant.ViewModels
{
    public class RowItemViewModel : BaseViewModel
    {
        #region Declarations

        private decimal total;
        private int count;
        private ObservableCollection<RowItemViewModel> extras;

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

        public Item Item { get; set; }

        public int Count
        {
            get => count;
            set
            {
                count = value;
                Total = count * Item.Price;
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

        #endregion
    }
}
