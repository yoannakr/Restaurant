using System.Linq;
using Prism.Commands;
using Restaurant.Views;
using Restaurant.Database.Models;

namespace Restaurant.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> menuCommand;
        private DelegateCommand<object> exitCommand;
        private BaseViewModel baseViewModel;
        private AllTablesViewModel alltablesViewModel;
        private AdminPanelViewModel adminPanelViewModel;
        private LoginViewModel loginViewModel;
        private User user;
        private bool adminBtnVisibility;

        #endregion

        #region Constructors

        public MenuViewModel(MainWindowViewModel mainWindowViewModel, User user)
        {
            MainWindowViewModel = mainWindowViewModel;
            BaseViewModel = AllTablesViewModel;
            User = user;
        }

        #endregion

        #region Properties

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

        public BaseViewModel BaseViewModel
        {
            get => baseViewModel;
            set
            {
                baseViewModel = value;
                OnPropertyChanged("BaseViewModel");
            }
        }

        public AllTablesViewModel AllTablesViewModel
        {
            get
            {
                if (alltablesViewModel == null)
                {
                    alltablesViewModel = new AllTablesViewModel(this);
                    AllTablesView alltablesView = new AllTablesView();

                    alltablesViewModel.View = alltablesView;

                    alltablesView.DataContext = alltablesViewModel;
                }

                return alltablesViewModel;
            }
        }

        public AdminPanelViewModel AdminPanelViewModel
        {
            get
            {
                if (adminPanelViewModel == null)
                {
                    adminPanelViewModel = new AdminPanelViewModel(this);
                    AdminPanelView adminPanelView = new AdminPanelView();

                    adminPanelViewModel.View = adminPanelView;

                    adminPanelView.DataContext = adminPanelViewModel;
                }

                return adminPanelViewModel;
            }
        }

        public LoginViewModel LoginViewModel
        {
            get
            {
                loginViewModel = new LoginViewModel(MainWindowViewModel);
                LoginView loginView = new LoginView();

                loginViewModel.View = loginView;

                loginView.DataContext = loginViewModel;

                return loginViewModel;
            }
        }

        public MainWindowViewModel MainWindowViewModel { get; set; }

        public User User
        {
            get => user;
            set
            {
                user = value;

                UserRole role = user.Roles
                                     .Where(u => u.Role.Name == "Админ")
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

        #endregion

        #region Methods

        private void ChangeView(object obj)
        {
            if (obj is BaseViewModel baseViewModel)
                BaseViewModel = baseViewModel;
        }

        private void Exit(object obj)
        {
            MainWindowViewModel.LoginViewModel = LoginViewModel;
            MainWindowViewModel.BaseViewModel = LoginViewModel;
        }

        #endregion
    }
}
