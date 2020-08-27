using System;
using Prism.Commands;
using Restaurant.Services;
using Restaurant.Services.Implementations;

namespace Restaurant.ViewModels
{
    public class CreateRoleViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> addRoleCommand;
        private DelegateCommand<object> returnCommand;
        private readonly IRoleService roleService;
        private string name;

        #endregion

        #region Constructors

        public CreateRoleViewModel(MenuViewModel menuViewModel, CreateUserViewModel createUserViewModel)
        {
            MenuViewModel = menuViewModel;
            CreateUserViewModel = createUserViewModel;
            roleService = new RoleService();
        }

        #endregion

        #region Properties

        public DelegateCommand<object> AddRoleCommand
        {
            get
            {
                if (addRoleCommand == null)
                    addRoleCommand = new DelegateCommand<object>(CreateRole, CanCreateRole);

                return addRoleCommand;
            }
        }

        public DelegateCommand<object> ReturnCommand
        {
            get
            {
                if (returnCommand == null)
                    returnCommand = new DelegateCommand<object>(Return);

                return returnCommand;
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                AddRoleCommand.RaiseCanExecuteChanged();
            }
        }

        public MenuViewModel MenuViewModel { get; set; }

        public CreateUserViewModel CreateUserViewModel { get; set; }

        #endregion

        #region Methods

        private void CreateRole(object obj)
        {
            roleService.CreateRole(Name);
            MenuViewModel.BaseViewModel = CreateUserViewModel;
        }

        private bool CanCreateRole(object arg)
        {
            return IsValid();
        }

        private bool IsValid()
        {
            if (String.IsNullOrEmpty(Name))
                return false;

            return true;
        }

        private void Return(object obj)
        {
            MenuViewModel.BaseViewModel = CreateUserViewModel;
        }

        #endregion
    }
}
