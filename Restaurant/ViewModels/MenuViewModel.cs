using Prism.Commands;
using Restaurant.Views;
using Restaurant.Database.Models;
using System.Linq;

namespace Restaurant.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> menuCommand;
        private DelegateCommand<object> exitCommand;
        private BaseViewModel baseViewModel;
        private BaseViewModel exitViewModel;
        private SalesViewModel salesViewModel;
        private AdminPanelViewModel adminPanelViewModel;
        private LoginViewModel loginViewModel;
        private User user;
        private bool adminBtnVisibility;

        #endregion

        #region Constructors

        public MenuViewModel(User user)
        {
            BaseViewModel = SalesViewModel;
            User = user;
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

        public User User
        {
            get => user;
            set
            {
                user = value;

                UserRole role = user.Roles
                                     .Where(u => u.Role.Name == "Admin")
                                     .FirstOrDefault();

                if (role != null)
                    AdminBtnVisibility = true;
                else
                    AdminBtnVisibility = false;
            }
        }

        public bool AdminBtnVisibility
        {
            get => adminBtnVisibility;
            set
            {
                adminBtnVisibility = value;
                OnPropertyChanged("AdminBtnVisibility");
            }
        }

        public DelegateCommand<object> MenuCommand
        {
            get
            {
                if (menuCommand == null)
                    menuCommand = new DelegateCommand<object>(ChangeView);

                return menuCommand;
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

        private void ChangeView(object obj)
        {
            if (obj is BaseViewModel baseViewModel)
                BaseViewModel = baseViewModel;
        }

        private void Exit(object obj)
        {
            ExitViewModel = LoginViewModel;
        }

        #endregion
    }
}
