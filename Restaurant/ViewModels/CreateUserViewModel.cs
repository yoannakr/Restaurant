﻿using System.Linq;
using Prism.Commands;
using System.Windows;
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
        private DelegateCommand<object> addRoleCommand;
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private BaseViewModel baseViewModel;
        private CreateRoleViewModel createRoleViewModel;
        private AdminPanelViewModel adminPanelViewModel;
        private string name;
        private string username;
        private string password;
        private string confirmPassword;
        private List<Role> userRoles;

        #endregion

        #region Constructors

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

        public DelegateCommand<object> AddRoleCommand
        {
            get
            {
                if (addRoleCommand == null)
                    addRoleCommand = new DelegateCommand<object>(AddRole);

                return addRoleCommand;
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
        }

        public AdminPanelViewModel AdminPanelViewModel
        {
            get
            {
                if (adminPanelViewModel == null)
                {
                    adminPanelViewModel = new AdminPanelViewModel();
                    AdminPanelView adminPanelView = new AdminPanelView();

                    adminPanelViewModel.View = adminPanelView;

                    adminPanelView.DataContext = adminPanelViewModel;
                }

                return adminPanelViewModel;
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

        public User User { get; set; }

        public List<Role> Roles
        {
            get
            {
                return roleService.GetAllRoles().ToList();
            }
        }

        public bool IsChecked { get; set; }

        public List<Role> UserRoles
        {
            get
            {
                if (userRoles == null)
                    userRoles = new List<Role>();

                return userRoles;
            }
        }

        #endregion

        #region Methods

        private void CreateUser(object obj)
        {
            User = userService.GetAllUsers()
                              .Where(u => u.Username == Username)
                              .FirstOrDefault();

            if (User != null)
            {
                MessageBox.Show("User with that username already exist.");
                return;
            }

            userService.CreateUser(Name, Username, Password, UserRoles);
            BaseViewModel = AdminPanelViewModel;
        }

        private bool CanCreateUser(object arg)
        {
            return IsValid();
        }


        private void CreateRole(object obj)
        {
            BaseViewModel = CreateRoleViewModel;
        }

        private bool IsValid()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Username) || UserRoles.Count == 0)
                return false;

            if (Password != ConfirmPassword)
                return false;

            return true;
        }

        private void AddRole(object obj)
        {
            Role role = obj as Role;

            if (IsChecked)
                UserRoles.Add(role);
            else
                UserRoles.Remove(role);

            AddUserCommand.RaiseCanExecuteChanged();
        }

        #endregion
    }
}
