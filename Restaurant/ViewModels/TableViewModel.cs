using System;
using Prism.Commands;
using System.Windows;
using Restaurant.Views;
using Restaurant.Services;
using Restaurant.Database.Models;
using Restaurant.Services.Implementations;
using Restaurant.Common.InstanceHolder;

namespace Restaurant.ViewModels
{
    public class TableViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> salesCommand;
        private DelegateCommand<object> updateTableCommand;
        private DelegateCommand<object> deleteTableCommand;
        private readonly ITableService tableService;
        private SalesViewModel salesViewModel;
        private UpdateTableViewModel updateTableViewModel;
        private const string GreenButtonPath = "../Images/green_button.png";
        private const string RedButtonPath = "../Images/red_button.png";
        private string imageSource;
        private string isTakenSource;

        #endregion

        #region Constructors

        public TableViewModel()
        {
            tableService = new TableService();
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

        public DelegateCommand<object> UpdateTableCommand
        {
            get
            {
                if (updateTableCommand == null)
                    updateTableCommand = new DelegateCommand<object>(UpdateTable);

                return updateTableCommand;
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

        private void SalesViewOpen(object obj)
        {
            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(SalesViewModel);
        }

        private void UpdateTable(object obj)
        {
            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(UpdateTableViewModel);
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
