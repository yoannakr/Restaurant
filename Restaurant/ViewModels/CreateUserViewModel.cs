﻿using System.Linq;
using Prism.Commands;
using System.Windows;
using Restaurant.Views;
using Restaurant.Services;
using System.Collections.Generic;
using Restaurant.Database.Models;
using System.Collections.ObjectModel;
using Restaurant.Services.Models.Role;
using Restaurant.Common.InstanceHolder;
using Restaurant.Services.Implementations;
using Restaurant.Services.Models.User;

namespace Restaurant.ViewModels
{
    public class CreateUserViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> addUserCommand;
        private DelegateCommand<object> newRoleCommand;
        private DelegateCommand<object> addRoleInCollectionCommand;
        private DelegateCommand<object> returnCommand;
        private readonly IUserService userService;
        private CreateRoleViewModel createRoleViewModel;
        private string name;
        private string username;
        private string password;
        private string confirmPassword;
        private ObservableCollection<RoleDto> roles;
        private List<RoleDto> userRoles;

        #endregion

        #region Constructors

        public CreateUserViewModel()
        {
            userService = new UserService();
        }

        #endregion

        #region Properties

        public DelegateCommand<object> AddUserCommand
        {
            get
            {
                if (addUserCommand == null)
                    addUserCommand = new DelegateCommand<object>(CreateUser, CanCreateUser);

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

        public DelegateCommand<object> AddRoleInCollectionCommand
        {
            get
            {
                if (addRoleInCollectionCommand == null)
                    addRoleInCollectionCommand = new DelegateCommand<object>(AddRoleInCollection);

                return addRoleInCollectionCommand;
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
                AddUserCommand.RaiseCanExecuteChanged();
            }
        }

        public string Username
        {
            get => username;
            set
            {
                username = value;
                AddUserCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                AddUserCommand.RaiseCanExecuteChanged();
            }
        }

        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                AddUserCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<RoleDto> Roles
        {
            get
            {
                if (roles == null)
                {
                    roles = CollectionInstance.Instance.Roles;

                    foreach (RoleDto role in roles)
                    {
                        role.IsChecked = false;
                    }
                }

                return roles;
            }
        }

        public List<RoleDto> UserRoles
        {
            get
            {
                if (userRoles == null)
                    userRoles = new List<RoleDto>();

                return userRoles;
            }
        }

        public CreateRoleViewModel CreateRoleViewModel
        {
            get
            {
                createRoleViewModel = new CreateRoleViewModel(this);
                CreateRoleView createRoleView = new CreateRoleView();

                createRoleViewModel.View = createRoleView;

                createRoleView.DataContext = createRoleViewModel;

                return createRoleViewModel;
            }
        }

        #endregion

        #region Methods

        private void CreateUser(object obj)
        {
            UserDto userDto = CollectionInstance.Instance
                                        .Users
                                        .Where(u => u.Username == Username)
                                        .FirstOrDefault();

            if (userDto != null)
            {
                MessageBox.Show("Потребителското име вече е заето.");
                return;
            }

            List<Role> roles = UserRoles
                                .Select(r => new Role()
                                {
                                    Id = r.Id,
                                    Name = r.Name
                                }).ToList();

            try
            {
                UserDto createdUser = userService.CreateUser(Name, Username, Password, roles);

                CollectionInstance.Instance.Users.Add(createdUser);
            }
            catch (System.Exception)
            {
                MessageBox.Show("Грешка с базата ! Опитайте отново !");
                return;
            }

            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(MenuViewModel.Instance.AdminPanelViewModel);
        }

        private bool CanCreateUser(object arg)
        {
            return IsValid();
        }

        private void CreateRole(object obj)
        {
            Password = null;
            ConfirmPassword = null;
            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(CreateRoleViewModel);
        }

        private bool IsValid()
        {
            if (string.IsNullOrEmpty(Name) ||
                string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(ConfirmPassword) ||
                UserRoles.Count == 0)
                return false;

            if (Password != ConfirmPassword)
                return false;

            return true;
        }

        private void AddRoleInCollection(object obj)
        {
            RoleDto role = obj as RoleDto;

            if (role.IsChecked)
                UserRoles.Add(role);
            else
                UserRoles.Remove(role);

            AddUserCommand.RaiseCanExecuteChanged();
        }

        private void Return(object obj)
        {
            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(MenuViewModel.Instance.AdminPanelViewModel);
        }

        #endregion
    }
}
