using Prism.Commands;
using Restaurant.Database.Models;
using Restaurant.Services;
using Restaurant.Services.Implementations;
using System;
using System.Linq;
using System.Windows;

namespace Restaurant.ViewModels
{
    public class UpdateTableViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> updateTableCommand;
        private DelegateCommand<object> returnCommand;
        private readonly ITableService tableService;
        private int seats;

        #endregion

        #region Constructors

        public UpdateTableViewModel(Table table)
        {
            Table = table;
            Number = table.Number;
            Seats = table.Seats;
            tableService = new TableService();
        }

        #endregion

        #region Properties

        public DelegateCommand<object> UpdateTableCommand
        {
            get
            {
                if (updateTableCommand == null)
                    updateTableCommand = new DelegateCommand<object>(UpdateTable, CanUpdateTable);

                return updateTableCommand;
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

        public Table Table { get; set; }

        public int Number { get; set; }

        public int Seats
        {
            get => seats;
            set
            {
                seats = value;
                UpdateTableCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Methods

        private void UpdateTable(object obj)
        {
            Table table = tableService.GetAllTables()
                                      .Where(t => t.Number == Number)
                                      .FirstOrDefault();

            if (table != null && Table.Id != table.Id)
            {
                MessageBox.Show("Маса с този номер вече съществува.");
                return;
            }

            try
            {
                Table updatedTable = new Table()
                {
                    Id = Table.Id,
                    Number = Number,
                    Seats = Seats,
                    IsTaken = Table.IsTaken
                };

                tableService.UpdateTable(updatedTable);
            }
            catch (Exception)
            {
                MessageBox.Show("Грешка с базата ! Опитайте отново !");
                return;
            }

            Table.Number = Number;
            Table.Seats = Seats;

            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(MenuViewModel.Instance.AdminPanelViewModel);
        }

        private bool CanUpdateTable(object arg)
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
