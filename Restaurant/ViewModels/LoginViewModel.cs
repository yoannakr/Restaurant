using System.Linq;
using Prism.Commands;
using Restaurant.Views;
using Restaurant.Services;
using Restaurant.Interfaces;
using System.Windows.Controls;
using Restaurant.Database.Models;
using Restaurant.Services.Implementations;
using System.Security.Cryptography;
using System.Text;

namespace Restaurant.ViewModels
{
    public class LoginViewModel : BaseViewModel, IHashPassword
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
            PasswordBox passwordBox = obj as PasswordBox;

            Password = ComputePasswordHashing(passwordBox.Password);

            User = userService.GetAllUsers()
                               .Where(u => u.Username == Username && u.Password == Password)
                               .FirstOrDefault();

            if (User == null)
                return;

            BaseViewModel = MenuViewModel;
        }

        public string ComputePasswordHashing(string rowPassword)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rowPassword));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString());
                }
                return builder.ToString();
            }
        }

        #endregion
    }
}
