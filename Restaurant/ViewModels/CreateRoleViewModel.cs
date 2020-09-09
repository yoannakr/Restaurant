using System;
using System.Windows;
using Prism.Commands;
using Restaurant.Services;
using Restaurant.Database.Models;
using Restaurant.Services.Models.Role;
using Restaurant.Common.InstanceHolder;
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

        public CreateRoleViewModel(BaseViewModel baseViewModel)
        {
            BaseViewModel = baseViewModel;
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

        public BaseViewModel BaseViewModel { get; set; }

        #endregion

        #region Methods

        private void CreateRole(object obj)
        {
            try
            {
                RoleDto role = roleService.CreateRole(Name);

                CollectionInstance.Instance.Roles.Add(role);
            }
            catch (Exception)
            {
                MessageBox.Show("Грешка с базата ! Опитайте отново !");
                return;
            }

            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(BaseViewModel);
        }

        private bool CanCreateRole(object arg)
        {
            return IsValid();
        }

        private bool IsValid()
        {
            if (string.IsNullOrEmpty(Name))
                return false;

            return true;
        }

        private void Return(object obj)
        {
            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(BaseViewModel);
        }

        #endregion
    }
}
