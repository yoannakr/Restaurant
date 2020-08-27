using System;
using Prism.Commands;
using Restaurant.Views;

namespace Restaurant.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> changeMainViewCommand;
        private BaseViewModel baseViewModel;
        private LoginViewModel loginViewModel;

        #endregion

        #region Constructors

        public MainWindowViewModel()
        {
            Instance = this;
            BaseViewModel = LoginViewModel;
        }

        #endregion

        #region Properties

        public DelegateCommand<object> ChangeMainViewCommand
        {
            get
            {
                if (changeMainViewCommand == null)
                    changeMainViewCommand = new DelegateCommand<object>(ChangeMainView);

                return changeMainViewCommand;
            }
        }

        public static MainWindowViewModel Instance { get; set; }

        public BaseViewModel BaseViewModel
        {
            get => baseViewModel;
            set
            {
                baseViewModel = value;
                OnPropertyChanged("BaseViewModel");
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
            set
            {
                loginViewModel = value;
            }
        }

        #endregion

        #region Methods

        private void ChangeMainView(object obj)
        {
            if (obj is BaseViewModel baseViewModel)
                BaseViewModel = baseViewModel;
        }

        #endregion
    }
}
