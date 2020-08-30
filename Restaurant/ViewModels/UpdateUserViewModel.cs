using System;
using System.Linq;
using System.Text;
using Prism.Commands;
using System.Windows;
using Restaurant.Views;
using Restaurant.Services;
using Restaurant.Interfaces;
using Restaurant.Database.Models;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Collections.ObjectModel;
using Restaurant.Services.Models.Role;
using Restaurant.Common.InstanceHolder;
using Restaurant.Services.Implementations;
using Restaurant.Common.Helpers;

namespace Restaurant.ViewModels
{
    public class UpdateUserViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> updateUserCommand;
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

        public UpdateUserViewModel(User user)
        {
            userService = new UserService();
            User = user;
        }

        #endregion

        #region Properties

        public DelegateCommand<object> UpdateUserCommand
        {
            get
            {
                if (updateUserCommand == null)
                    updateUserCommand = new DelegateCommand<object>(UpdateUser, CanUpdateUser);

                return updateUserCommand;
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

        public User User { get; set; }

        public string Name
        {
            get
            {
                if (name == null)
                    name = User.Name;

                return name;
            }
            set
            {
                name = value;
                UpdateUserCommand.RaiseCanExecuteChanged();
            }
        }

        public string Username
        {
            get
            {
                if (username == null)
                    username = User.Username;

                return username;
            }
            set
            {
                username = value;
                UpdateUserCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = HashingPasswordHelper.ComputePasswordHashing(value);
                UpdateUserCommand.RaiseCanExecuteChanged();
            }
        }

        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = HashingPasswordHelper.ComputePasswordHashing(value);
                UpdateUserCommand.RaiseCanExecuteChanged();
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
                        RoleDto roleDto = UserRoles.Where(r => r.Id == role.Id)
                                                   .FirstOrDefault();
                        if (roleDto == null)
                            role.IsChecked = false;
                        else
                            role.IsChecked = true;
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
                {
                    List<RoleDto> checkedRoles = User.Roles.Select(ur => ur.Role)
                                                           .Select(r => new RoleDto()
                                                           {
                                                               Id = r.Id,
                                                               Name = r.Name
                                                           }).ToList();

                    userRoles = checkedRoles;
                }

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

        private void UpdateUser(object obj)
        {
            UserViewModel user = CollectionInstance.Instance
                                        .Users
                                        .Where(u => u.User.Username == Username)
                                        .FirstOrDefault();

            if (user != null && User.Id != user.User.Id)
            {
                MessageBox.Show("Потребителското име вече е заето.");
                return;
            }

            List<Role> roles = UserRoles
                                    .Select(r => new Role()
                                    {
                                        Id = r.Id,
                                        Name = r.Name
                                    })
                                    .ToList();

            List<UserRole> userRoles = roles.Select(r => new UserRole()
            {
                UserId = User.Id,
                RoleId = r.Id,
                Role = r
            }).ToList();

            try
            {
                User updatedUser = new User()
                {
                    Id = User.Id,
                    Name = Name,
                    Username = Username,
                    Password = Password
                };

                userService.UpdateUser(updatedUser, userRoles);
            }
            catch (Exception)
            {
                MessageBox.Show("Грешка с базата ! Опитайте отново !");
                return;
            }

            User.Name = Name;
            User.Username = Username;
            User.Password = Password;
            User.Roles = userRoles;

            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(MenuViewModel.Instance.AdminPanelViewModel);
        }

        private bool CanUpdateUser(object arg)
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
            {
                RoleDto removeRole = UserRoles.FirstOrDefault(r => r.Id == role.Id);
                UserRoles.Remove(removeRole);
            }

            UpdateUserCommand.RaiseCanExecuteChanged();
        }

        private void Return(object obj)
        {
            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(MenuViewModel.Instance.AdminPanelViewModel);
        }

        #endregion
    }
}
