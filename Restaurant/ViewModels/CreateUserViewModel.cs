using System.Linq;
using Prism.Commands;
using Restaurant.Views;
using Restaurant.Services;
using System.Collections.Generic;
using Restaurant.Database.Models;
using Restaurant.Services.Implementations;

namespace Restaurant.ViewModels
{
    public class CreateUserViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> addUserCommand;
        private DelegateCommand<object> newRoleCommand;
        private IUserService userService;
        private IRoleService roleService;
        private BaseViewModel baseViewModel;
        private CreateRoleViewModel createRoleViewModel;

        #endregion

        #region Constructor

        public CreateUserViewModel()
        {
            userService = new UserService();
            roleService = new RoleService();
        }

        #endregion

        #region Properties

        public DelegateCommand<object> AddUserCommand 
        {
            get
            {
                if (addUserCommand == null)
                    addUserCommand = new DelegateCommand<object>(CreateUser);

                return addUserCommand;
            }
        }

        public DelegateCommand<object> NewRoleCommand
        {
            get
            {
                if (newRoleCommand == null)
                    newRoleCommand = new DelegateCommand<object>(CreateRole);

                return newRoleCommand;
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

        public CreateRoleViewModel CreateRoleViewModel 
        {
            get
            {
                if (createRoleViewModel == null)
                {
                    createRoleViewModel = new CreateRoleViewModel();
                    CreateRoleView createRoleView = new CreateRoleView();

                    createRoleViewModel.View = createRoleView;

                    createRoleView.DataContext = createRoleViewModel;
                }

                return createRoleViewModel;
            }
            set
            {
                createRoleViewModel = value;
                OnPropertyChanged("CreateRoleViewModel");
            }
        }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        public Role Role { get; set; }

        public string ConfirmPassword { get; set; }

        public List<Role> Roles
        {
            get
            {
                return roleService.GetAllRoles().ToList();
            }
        }

        #endregion

        #region Methods

        private void CreateUser(object obj)
        {
            userService.CreateUser(Name, Username, Password, Role.Id);
        }

        private void CreateRole(object obj)
        {
            BaseViewModel = CreateRoleViewModel;
        }

        #endregion
    }
}
