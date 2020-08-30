using System;
using Prism.Commands;
using System.Windows;
using Restaurant.Views;
using Restaurant.Services;
using Restaurant.Database.Models;
using System.Collections.Generic;
using Restaurant.Common.InstanceHolder;
using Restaurant.Services.Implementations;

namespace Restaurant.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> updateUserCommand;
        private DelegateCommand<object> deleteUserCommand;
        private readonly IUserService userService;
        private UpdateUserViewModel updateUserViewModel;
        private string imageSource;
        private string roles;

        #endregion

        #region Constructors

        public UserViewModel()
        {
            userService = new UserService();
        }

        #endregion

        #region Properties

        public DelegateCommand<object> UpdateUserCommand
        {
            get
            {
                if (updateUserCommand == null)
                    updateUserCommand = new DelegateCommand<object>(UpdateUser);

                return updateUserCommand;
            }
        }

        public DelegateCommand<object> DeleteUserCommand
        {
            get
            {
                if (deleteUserCommand == null)
                    deleteUserCommand = new DelegateCommand<object>(DeleteUser);

                return deleteUserCommand;
            }
        }

        public User User { get; set; }

        public string ImageSource
        {
            get
            {
                if (imageSource == null)
                    imageSource = "../Images/user_icon.png";

                return imageSource;
            }
        }

        public string Roles
        {
            get
            {
                roles = string.Empty;

                List<string> allRolesName = new List<string>();

                foreach (UserRole userRole in User.Roles)
                {
                    allRolesName.Add(userRole.Role.Name);
                }

                roles = string.Join(", ", allRolesName);

                return roles;
            }
        }

        public UpdateUserViewModel UpdateUserViewModel
        {
            get
            {
                updateUserViewModel = new UpdateUserViewModel(User);
                UpdateUserView updateUserView = new UpdateUserView();

                updateUserViewModel.View = updateUserView;

                updateUserView.DataContext = updateUserViewModel;

                return updateUserViewModel;
            }
        }

        #endregion

        #region Methods

        private void UpdateUser(object obj)
        {
            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(UpdateUserViewModel);
        }

        private void DeleteUser(object obj)
        {
            if (MessageBox.Show("Сигурни ли сте, че искате да изтриете този потребител ?",
                    "Потвърждение", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            try
            {
                userService.DeleteUser(User);
            }
            catch (Exception)
            {
                MessageBox.Show("Грешка с базата ! Опитайте отново !");
                return;
            }

            CollectionInstance.Instance.Users.Remove(this);
        }

        #endregion
    }
}
