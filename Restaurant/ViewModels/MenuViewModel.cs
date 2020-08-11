using System;
using Prism.Commands;
using Restaurant.Views;
using System.Windows.Input;

namespace Restaurant.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> salesCommand;
        private DelegateCommand<object> adminPanelCommand;
        private BaseViewModel baseViewModel;
        private SalesViewModel salesViewModel;
        private AdminPanelViewModel adminPanelViewModel;

        #endregion

        #region Constructor

        public MenuViewModel()
        {
            BaseViewModel = SalesViewModel;
        }

        #endregion

        #region Properties

        public BaseViewModel BaseViewModel
        {
            get => baseViewModel;
            set
            {
                baseViewModel = value;
                OnPropertyChanged("BaseViewModel");
            }
        }

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
            set
            {
                salesViewModel = value;
                OnPropertyChanged("SalesViewModel");
            }
        }

        public AdminPanelViewModel AdminPanelViewModel
        {
            get
            {
                
                    adminPanelViewModel = new AdminPanelViewModel();
                    AdminPanelView adminPanelView = new AdminPanelView();

                    adminPanelViewModel.View = adminPanelView;

                    adminPanelView.DataContext = adminPanelViewModel;


                return adminPanelViewModel;
            }
            set
            {
                adminPanelViewModel = value;
                OnPropertyChanged("AdminPanelViewModel");
            }
        }

        public DelegateCommand<object> SalesCommand
        {
            get
            {
                if (salesCommand == null)
                    salesCommand = new DelegateCommand<object>(SalesViewOpen);

                return salesCommand;
            }
        }

        public DelegateCommand<object> AdminPanelCommand
        {
            get
            {
                if (adminPanelCommand == null)
                    adminPanelCommand = new DelegateCommand<object>(AdminPanelViewOpen);

                return adminPanelCommand;
            }
        }

        #endregion

        #region Methods

        private void SalesViewOpen(object obj)
        {
            BaseViewModel = SalesViewModel;
        }

        private void AdminPanelViewOpen(object obj)
        {
            BaseViewModel = AdminPanelViewModel;
        }

        #endregion
    }
}
