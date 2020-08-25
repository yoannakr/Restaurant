using Prism.Commands;
using Restaurant.Views;
using Restaurant.Services;
using Restaurant.Database.Models;
using Restaurant.Services.Implementations;

namespace Restaurant.ViewModels
{
    public class TableViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> salesCommand;
        private SalesViewModel salesViewModel;
        private const string GreenButtonPath = "../Images/green_button.png";
        private const string RedButtonPath = "../Images/red_button.png";
        private string isTakenSource;
        private readonly ITableService tableService;

        #endregion

        #region Constructors

        public TableViewModel(MenuViewModel menuViewModel)
        {
            tableService = new TableService();
            MenuViewModel = menuViewModel;
        }

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

        public string ImageSource { get; } = "../Images/table_icon.png";

        public string IsTakenSource
        {
            get
            {
                if (isTakenSource == null)
                    isTakenSource = GreenButtonPath;

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
                    salesViewModel = new SalesViewModel();
                    SalesView salesView = new SalesView();

                    salesViewModel.View = salesView;

                    salesView.DataContext = salesViewModel;
                }

                return salesViewModel;
            }
        }

        public MenuViewModel MenuViewModel { get; set; }

        #endregion

        #region Methods

        private void SalesViewOpen(object obj)
        {
            MenuViewModel.BaseViewModel = SalesViewModel;
            IsTakenSource = RedButtonPath;
        }

        #endregion
    }
}
