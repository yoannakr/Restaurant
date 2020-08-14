using System;
using System.Linq;
using Prism.Commands;
using System.Security;
using Restaurant.Views;
using Restaurant.Services;
using Restaurant.Interfaces;
using Restaurant.Database.Models;
using System.Runtime.InteropServices;
using Restaurant.Services.Implementations;

namespace Restaurant.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> loginCommand;
        private readonly IUserService userService;
        private BaseViewModel baseViewModel;
        private MenuViewModel menuViewModel;

        #endregion

        #region Constructors

        public LoginViewModel()
        {
            userService = new UserService();
        }

        #endregion

        #region Properties

        public DelegateCommand<object> LoginCommand
        {
            get
            {
                if (loginCommand == null)
                    loginCommand = new DelegateCommand<object>(Login);

                return loginCommand;
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

        public MenuViewModel MenuViewModel
        {
            get
            {
                if (menuViewModel == null)
                {
                    menuViewModel = new MenuViewModel();
                    MenuView menuView = new MenuView();

                    menuViewModel.View = menuView;

                    menuView.DataContext = menuViewModel;
                }

                return menuViewModel;
            }
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public User User { get; set; }

        #endregion

        #region Methods

        private void Login(object obj)
        {
            IHavePassword passwordContainer = obj as IHavePassword;

            if (passwordContainer != null)
            {
                SecureString secureString = passwordContainer.Password;
                //MessageBox.Show($"{ConvertToUnsecureString(secureString)}");
                Password = ConvertToUnsecureString(secureString);
            }

            User = userService.GetAllUsers()
                               .Where(u => u.Username == Username && u.Password == Password)
                               .FirstOrDefault();

            if (User == null)
                return;

            BaseViewModel = MenuViewModel;
        }

        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        #endregion
    }
}
