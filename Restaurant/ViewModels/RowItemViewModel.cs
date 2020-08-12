﻿using Prism.Commands;
using Restaurant.Database.Models;
using System.Collections.ObjectModel;

namespace Restaurant.ViewModels
{
    public class RowItemViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> deleteExtraCommand;
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

        public DelegateCommand<object> DeleteExtraCommand
        {
            get
            {
                if (deleteExtraCommand == null)
                    deleteExtraCommand = new DelegateCommand<object>(DeleteExtra);

                return deleteExtraCommand;
            }
        }

        #endregion

        #region Methods

        private void DeleteExtra(object obj)
        {
            RowItemViewModel item = obj as RowItemViewModel;

            Extras.Remove(item);
        }


        #endregion
    }
}
