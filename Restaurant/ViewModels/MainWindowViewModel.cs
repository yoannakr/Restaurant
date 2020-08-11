using Restaurant.Views;

namespace Restaurant.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Declarations

        private BaseViewModel baseViewModel;
        private LoginViewModel loginViewModel;

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            BaseViewModel = LoginViewModel;
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

        #endregion
    }
}
