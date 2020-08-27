using System.Text;
using System.Linq;
using System.Windows;
using Prism.Commands;
using Restaurant.Views;
using Restaurant.Services;
using Restaurant.Interfaces;
using Restaurant.Database.Models;
using System.Security.Cryptography;
using Restaurant.Services.Implementations;

namespace Restaurant.ViewModels
{
    public class LoginViewModel : BaseViewModel, IHashPassword
    {
        #region Declarations

        private DelegateCommand<object> loginCommand;
        private readonly IUserService userService;
        private MenuViewModel menuViewModel;
        private string password;

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

        public MenuViewModel MenuViewModel
        {
            get
            {
                if (menuViewModel == null)
                {
                    menuViewModel = new MenuViewModel(User);
                    MenuView menuView = new MenuView();

                    menuViewModel.View = menuView;

                    menuView.DataContext = menuViewModel;
                }

                return menuViewModel;
            }
        }

        public string Username { get; set; }

        public string Password
        {
            get => password;
            set
            {
                password = ComputePasswordHashing(value);
            }
        }

        public User User { get; set; }

        #endregion

        #region Methods

        private void Login(object obj)
        {
            User = userService.GetAllUsers()
                               .Where(u => u.Username == Username && u.Password == Password)
                               .FirstOrDefault();

            if (User == null)
            {
                MessageBox.Show("Грешно потребителско име и/или парола !");
                return;
            }

            MainWindowViewModel.Instance.ChangeMainViewCommand.Execute(MenuViewModel);
        }

        public string ComputePasswordHashing(string rowPassword)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rowPassword));

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
