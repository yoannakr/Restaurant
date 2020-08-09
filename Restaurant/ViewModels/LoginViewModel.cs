using System;
using System.Windows;
using Prism.Commands;
using System.Security;
using Restaurant.Views;
using System.Windows.Input;
using Restaurant.Interfaces;
using System.Runtime.InteropServices;

namespace Restaurant.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Declarations

        private ICommand loginCommand;
        private BaseViewModel baseViewModel;
        private MenuViewModel menuViewModel;

        #endregion

        #region Properties

        public ICommand LoginCommand
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
            set
            {
                menuViewModel = value;
                OnPropertyChanged("MenuViewModel");
            }
        }

        #endregion

        #region Methods

        private void Login(object obj)
        {
            IHavePassword passwordContainer = obj as IHavePassword;

            if (passwordContainer != null)
            {
                SecureString secureString = passwordContainer.Password;
                MessageBox.Show($"{ConvertToUnsecureString(secureString)}");
            }

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
