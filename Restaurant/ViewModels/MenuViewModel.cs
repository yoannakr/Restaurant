using Prism.Commands;
using Restaurant.Views;
using System;

namespace Restaurant.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> salesCommand;
        private DelegateCommand<object> adminPanelCommand;
        private DelegateCommand<object> exitCommand;
        private BaseViewModel baseViewModel;
        private BaseViewModel exitViewModel;
        private SalesViewModel salesViewModel;
        private AdminPanelViewModel adminPanelViewModel;
        private LoginViewModel loginViewModel;

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

        public BaseViewModel ExitViewModel
        {
            get => exitViewModel;
            set
            {
                exitViewModel = value;
                OnPropertyChanged("ExitViewModel");
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
        }

        public LoginViewModel LoginViewModel
        {
            get
            {
                if (loginViewModel == null)
                {
                    loginViewModel = new LoginViewModel();
                    LoginView loginView = new LoginView();

                    loginViewModel.View = loginView;

                    loginView.DataContext = loginViewModel;
                }

                return loginViewModel;
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

        public DelegateCommand<object> ExitCommand
        {
            get
            {
                if (exitCommand == null)
                    exitCommand = new DelegateCommand<object>(Exit);

                return exitCommand;
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

        private void Exit(object obj)
        {
            ExitViewModel = LoginViewModel;
        }

        #endregion
    }
}
