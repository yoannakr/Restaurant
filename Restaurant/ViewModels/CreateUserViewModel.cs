using Prism.Commands;
using Restaurant.Services;
using System.Windows.Input;
using Restaurant.Database.Models;
using Restaurant.Services.Implementations;

namespace Restaurant.ViewModels
{
    public class CreateUserViewModel : BaseViewModel
    {
        #region Declarations

        private ICommand addUserCommand;
        private IUserService userService;

        #endregion

        #region Constructor

        public CreateUserViewModel()
        {
            userService = new UserService();
        }

        #endregion

        #region Properties

        public ICommand AddUserCommand 
        {
            get
            {
                if (addUserCommand == null)
                    addUserCommand = new DelegateCommand<object>(CreateUser);

                return addUserCommand;
            }
        }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public Role Role 
        {
            get
            {
                return new Role()
                {
                    Name = "Admin"
                };
            }
        }

        #endregion

        #region Methods

        private void CreateUser(object obj)
        {
            userService.CreateUser(Name, Username, Password, Role);
        }

        #endregion
    }
}
