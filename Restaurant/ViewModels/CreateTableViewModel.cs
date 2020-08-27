using System.Linq;
using System.Windows;
using Prism.Commands;
using Restaurant.Services;
using Restaurant.Database.Models;
using Restaurant.Services.Implementations;

namespace Restaurant.ViewModels
{
    public class CreateTableViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> addTableCommand;
        private DelegateCommand<object> returnCommand;
        private readonly ITableService tableService;
        private int seats;

        #endregion

        #region Constructors

        public CreateTableViewModel()
        {
            tableService = new TableService();
        }

        #endregion

        #region Properties

        public DelegateCommand<object> AddTableCommand
        {
            get
            {
                if (addTableCommand == null)
                    addTableCommand = new DelegateCommand<object>(CreateTable, CanCreateTable);

                return addTableCommand;
            }
        }

        public DelegateCommand<object> ReturnCommand
        {
            get
            {
                if (returnCommand == null)
                    returnCommand = new DelegateCommand<object>(Return);

                return returnCommand;
            }
        }

        public long Number { get; set; }

        public int Seats
        {
            get => seats;
            set
            {
                seats = value;
                AddTableCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Methods

        private void CreateTable(object obj)
        {
            Table table = tableService.GetAllTables()
                                      .Where(t => t.Number == Number)
                                      .FirstOrDefault();

            if (table != null)
            {
                MessageBox.Show("Маса с този номер вече съществува.");
                return;
            }

            tableService.CreateTable(Number, Seats);
            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(MenuViewModel.Instance.AdminPanelViewModel);
        }

        private bool CanCreateTable(object arg)
        {
            return IsValid();
        }

        private bool IsValid()
        {
            if (Seats <= 0)
                return false;

            return true;
        }

        private void Return(object obj)
        {
            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(MenuViewModel.Instance.AdminPanelViewModel);
        }

        #endregion
    }
}
