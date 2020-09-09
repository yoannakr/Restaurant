using System;
using System.Linq;
using Prism.Commands;
using System.Windows;
using Restaurant.Services;
using Restaurant.Database.Models;
using Restaurant.Services.Implementations;
using Restaurant.Services.Models.Table;

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

        public UpdateTableViewModel(TableDto tableDto)
        {
            TableDto = tableDto;
            Number = tableDto.Number;
            Seats = tableDto.Seats;
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

        public TableDto TableDto { get; set; }

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
            TableDto tableDto = tableService.GetAllTables()
                                      .Where(t => t.Number == Number)
                                      .FirstOrDefault();

            if (tableDto != null && TableDto.Id != tableDto.Id)
            {
                MessageBox.Show("Маса с този номер вече съществува.");
                return;
            }

            try
            {
                Table updatedTable = new Table()
                {
                    Id = TableDto.Id,
                    Number = Number,
                    Seats = Seats,
                    IsTaken = TableDto.IsTaken
                };

                tableService.UpdateTable(updatedTable);
            }
            catch (Exception)
            {
                MessageBox.Show("Грешка с базата ! Опитайте отново !");
                return;
            }

            TableDto.Number = Number;
            TableDto.Seats = Seats;

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
            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(MenuViewModel.Instance.AdminPanelViewModel.UpdateOrDeleteTableViewModel);
        }

        #endregion
    }
}
