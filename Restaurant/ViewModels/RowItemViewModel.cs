using Prism.Commands;
using Restaurant.Database.Models;
using System.Collections.ObjectModel;

namespace Restaurant.ViewModels
{
    public class RowItemViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> deleteExtraCommand;
        private DelegateCommand<object> showOrHideListCommand;
        private ObservableCollection<RowItemViewModel> extras;
        private const string ArrowUp = @"C:\Users\User\Source\Repos\Restaurant\Restaurant\Images\arrow_up.png";
        private const string ArrowDown = @"C:\Users\User\Source\Repos\Restaurant\Restaurant\Images\arrow_down.png";
        private bool showListBtnVisibility;
        private bool listVisibility;
        private string iconSource;
        private decimal total;
        private int count;

        #endregion

        #region Constructors

        public RowItemViewModel(SelectedItemViewModel selectedItemViewModel)
        {
            SelectedItemViewModel = selectedItemViewModel;
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

        public SelectedItemViewModel SelectedItemViewModel { get; set; }

        public RowItemViewModel RowItemViewModelex { get; set; }

        public Item Item { get; set; }

        public bool IsDown { get; set; }

        public int Count
        {
            get => count;
            set
            {
                if (value <= 0)
                {
                    value = 1;
                }
                count = value;
                SelectedItemViewModel.Total -= Total;
                Total = count * Item.Price;
                SelectedItemViewModel.Total += Total;
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

        public string IconSource
        {
            get
            {
                if (iconSource == null)
                {
                    iconSource = ArrowDown;
                    IsDown = true;
                }

                return iconSource;
            }
            set
            {
                iconSource = value;
                OnPropertyChanged("IconSource");
            }
        }

        public bool ShowListBtnVisibility
        {
            get
            {
                if (Extras.Count != 0)
                    showListBtnVisibility = true;

                return showListBtnVisibility;
            }
            set
            {
                showListBtnVisibility = value;
                OnPropertyChanged("ShowListBtnVisibility");
            }
        }

        public bool ListVisibility
        {
            get => listVisibility;
            set
            {
                listVisibility = value;
                OnPropertyChanged("ListVisibility");
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

        public DelegateCommand<object> ShowOrHideListCommand
        {
            get
            {
                if (showOrHideListCommand == null)
                    showOrHideListCommand = new DelegateCommand<object>(ShowOrHideList);

                return showOrHideListCommand;
            }
        }

        #endregion

        #region Methods

        private void DeleteExtra(object obj)
        {
            RowItemViewModel item = obj as RowItemViewModel;

            RowItemViewModelex.Extras.Remove(item);

            SelectedItemViewModel.Total -= item.Total;

            if (RowItemViewModelex.Extras.Count == 0)
                RowItemViewModelex.ShowListBtnVisibility = false;
        }

        private void ShowOrHideList(object obj)
        {
            if (IsDown)
            {
                IconSource = ArrowUp;
            }
            else
            {
                IconSource = ArrowDown;
            }

            IsDown = !IsDown;
            ListVisibility = !ListVisibility;
        }

        #endregion
    }
}
