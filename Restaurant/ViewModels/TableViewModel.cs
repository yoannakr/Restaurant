using Prism.Commands;
using Restaurant.Views;
using Restaurant.Database.Models;

namespace Restaurant.ViewModels
{
    public class TableViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> salesCommand;
        private SalesViewModel salesViewModel;
        private const string GreenButtonPath = "../Images/green_button.png";
        private const string RedButtonPath = "../Images/red_button.png";
        private string imageSource;
        private string isTakenSource;

        #endregion

        #region Properties

        public DelegateCommand<object> SalesCommand
        {
            get
            {
                if (salesCommand == null)
                    salesCommand = new DelegateCommand<object>(SalesViewOpen);

                return salesCommand;
            }
        }

        public Table Table { get; set; }

        public string ImageSource
        {
            get
            {
                if (imageSource == null)
                    imageSource = "../Images/table_icon.png";

                return imageSource;
            }
        }

        public string IsTakenSource
        {
            get
            {
                if (!IsTaken)
                    isTakenSource = GreenButtonPath;
                else
                    isTakenSource = RedButtonPath;

                return isTakenSource;
            }
            set
            {
                isTakenSource = value;
                OnPropertyChanged("IsTakenSource");
            }
        }

        public bool IsTaken { get; set; }

        public SalesViewModel SalesViewModel
        {
            get
            {
                if (salesViewModel == null)
                {
                    salesViewModel = new SalesViewModel(this);
                    SalesView salesView = new SalesView();

                    salesViewModel.View = salesView;

                    salesView.DataContext = salesViewModel;
                }

                return salesViewModel;
            }
        }

        #endregion

        #region Methods

        private void SalesViewOpen(object obj)
        {
            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(SalesViewModel);
        }

        #endregion
    }
}
