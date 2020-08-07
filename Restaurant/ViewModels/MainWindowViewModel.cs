using System.Windows;
using Prism.Commands;
using System.Windows.Input;
using Restaurant.Interfaces;
using System.Security;
using System;
using System.Runtime.InteropServices;

namespace Restaurant.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ICommand loginCommand;

        public ICommand LoginCommand 
        {
            get
            {
                if (loginCommand == null)
                    loginCommand = new DelegateCommand<object>(Login);

                return loginCommand;
            }
        }

        private void Login(object obj)
        {
            IHavePassword passwordContainer = obj as IHavePassword; 

            if (passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
                MessageBox.Show($"{ConvertToUnsecureString(secureString)}");
            }
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
    }
}
