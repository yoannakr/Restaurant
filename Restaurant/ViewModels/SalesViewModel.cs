﻿using Prism.Commands;
using System.Windows;
using Restaurant.Views;

namespace Restaurant.ViewModels
{
    public class SalesViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand takeTableCommand;
        private AllItemsViewModel allItemViewModel;
        private SelectedItemViewModel selectedItemViewModel;
        private const string takeTableText = "Вземи маса";
        private const string freeTableText = "Освободи маса";
        private string tableText;

        #endregion

        #region Constructors

        public SalesViewModel(TableViewModel tableViewModel)
        {
            TableViewModel = tableViewModel;
        }

        #endregion

        #region Properties

        public DelegateCommand TakeTableCommand
        {
            get
            {
                if (takeTableCommand == null)
                    takeTableCommand = new DelegateCommand(TakeTable);

                return takeTableCommand;
            }
        }

        public AllItemsViewModel AllItemViewModel
        {
            get
            {
                if (allItemViewModel == null)
                {
                    allItemViewModel = new AllItemsViewModel(this);
                    AllItemsView allItemView = new AllItemsView();

                    allItemViewModel.View = allItemView;

                    allItemView.DataContext = allItemViewModel;
                }

                return allItemViewModel;
            }
        }

        public SelectedItemViewModel SelectedItemViewModel
        {
            get
            {
                if (selectedItemViewModel == null)
                {
                    selectedItemViewModel = new SelectedItemViewModel(TableViewModel);
                    SelectedItemView selectedItemView = new SelectedItemView();

                    selectedItemViewModel.View = selectedItemView;

                    selectedItemView.DataContext = selectedItemViewModel;
                }

                return selectedItemViewModel;
            }
        }

        public TableViewModel TableViewModel { get; set; }

        public string TableText
        {
            get
            {
                if (TableViewModel.TableDto.IsTaken)
                    tableText = freeTableText;
                else
                    tableText = takeTableText;

                return tableText;
            }
            set
            {
                tableText = value;
                OnPropertyChanged("TableText");
            }
        }

        #endregion

        #region Methods

        private void TakeTable()
        {
            if (SelectedItemViewModel.Items.Count != 0 && TableViewModel.TableDto.IsTaken)
            {
                MessageBox.Show("Не може да освободите масата ! Има избрани продукти.");
                return;
            }

            TableViewModel.TableDto.IsTaken = !TableViewModel.TableDto.IsTaken;

            if (TableViewModel.TableDto.IsTaken)
                TableText = freeTableText;
            else
                TableText = takeTableText;
        }

        #endregion
    }
}
