using System;
using Prism.Commands;
using System.Windows;
using Restaurant.Views;
using Restaurant.Services;
using Restaurant.Database.Models;
using Restaurant.Common.InstanceHolder;
using Restaurant.Services.Implementations;

namespace Restaurant.ViewModels
{
    public class TableViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> changeMenuViewCommand;
        private DelegateCommand<object> deleteTableCommand;
        private readonly ITableService tableService;
        private SalesViewModel salesViewModel;
        private UpdateTableViewModel updateTableViewModel;
        private const string GreenButtonPath = "../Images/green_button.png";
        private const string RedButtonPath = "../Images/red_button.png";
        private string imageSource = "../Images/table_icon.png";
        private string isTakenSource;

        #endregion

        #region Constructors

        public TableViewModel()
        {
            tableService = new TableService();
        }

        #endregion

        #region Properties

        public DelegateCommand<object> ChangeMenuViewCommand
        {
            get
            {
                if (changeMenuViewCommand == null)
                    changeMenuViewCommand = new DelegateCommand<object>(ChangeMenuView);

                return changeMenuViewCommand;
            }
        }

        public DelegateCommand<object> DeleteTableCommand
        {
            get
            {
                if (deleteTableCommand == null)
                    deleteTableCommand = new DelegateCommand<object>(DeleteTable);

                return deleteTableCommand;
            }
        }

        public Table Table { get; set; }

        public string ImageSource => imageSource;

        public string IsTakenSource
        {
            get
            {
                if (!Table.IsTaken)
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

        public UpdateTableViewModel UpdateTableViewModel
        {
            get
            {
                if (updateTableViewModel == null)
                {
                    updateTableViewModel = new UpdateTableViewModel(Table);
                    UpdateTableView updateTableView = new UpdateTableView();

                    updateTableViewModel.View = updateTableView;

                    updateTableView.DataContext = updateTableViewModel;
                }

                return updateTableViewModel;
            }
        }

        #endregion

        #region Methods

        private void ChangeMenuView(object obj)
        {
            if (obj is BaseViewModel baseViewModel)
                MenuViewModel.Instance.ChangeMenuViewCommand.Execute(baseViewModel);
        }

        private void DeleteTable(object obj)
        {
            if (Table.IsTaken)
            {
                MessageBox.Show("Не може да изтриете масата ! Първо я освободете.");
                return;
            }

            if (MessageBox.Show("Сигурни ли сте, че искате да изтриете тази маса ?",
                    "Потвърждение", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            try
            {
                tableService.DeleteTable(Table);
            }
            catch (Exception)
            {
                MessageBox.Show("Грешка с базата ! Опитайте отново !");
                return;
            }

            CollectionInstance.Instance.Tables.Remove(this);
        }

        #endregion
    }
}
