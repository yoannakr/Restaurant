using System;
using System.Collections.ObjectModel;
using System.Windows;
using Prism.Commands;
using Restaurant.Common.InstanceHolder;
using Restaurant.Database.Models;
using Restaurant.Services;
using Restaurant.Services.Implementations;
using Restaurant.Services.Models.User;
using Restaurant.Views;

namespace Restaurant.ViewModels
{
    public class UpdateOrDeleteUserViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> updateUserCommand;
        private DelegateCommand<object> deleteUserCommand;
        private ObservableCollection<UserDto> users;
        private readonly IUserService userService;
        private UpdateUserViewModel updateUserViewModel;

        #endregion

        #region Constructors

        public UpdateOrDeleteUserViewModel()
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

        public UpdateUserViewModel UpdateUserViewModel
        {
            get
            {
                updateUserViewModel = new UpdateUserViewModel(UserDto);
                UpdateUserView updateUserView = new UpdateUserView();

                updateUserViewModel.View = updateUserView;

                updateUserView.DataContext = updateUserViewModel;

                return updateUserViewModel;
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

        public ObservableCollection<UserDto> Users
        {
            get
            {
                if (users == null)
                    users = CollectionInstance.Instance.Users;

                return users;
            }
        }

        public UserDto UserDto { get; set; }

        #endregion

        #region Methods

        private void UpdateUser(object obj)
        {
            if (!(obj is UserDto userDto))
            {
                MessageBox.Show("Грешка в системата !");
                return;
            }

            UserDto = userDto;
            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(UpdateUserViewModel);
        }

        private void DeleteUser(object obj)
        {
            if (!(obj is UserDto userDto))
            {
                MessageBox.Show("Грешка в системата !");
                return;
            }

            if (userDto.Id == MenuViewModel.Instance.UserDto.Id)
            {
                if (MessageBox.Show("Сигурни ли сте, че искате да изтриете своя профил ?",
                    "Потвърждение", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }
            else
            {
                if (MessageBox.Show("Сигурни ли сте, че искате да изтриете този потребител ?",
                  "Потвърждение", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }

            try
            {
                User deletedUser = new User()
                {
                    Id = userDto.Id
                };

                userService.DeleteUser(deletedUser);
            }
            catch (Exception)
            {
                MessageBox.Show("Грешка с базата ! Опитайте отново !");
                return;
            }

            if (userDto.Id == MenuViewModel.Instance.UserDto.Id)
                MenuViewModel.Instance.ExitCommand.Execute();

            CollectionInstance.Instance.Users.Remove(userDto);
        }

        #endregion
    }
}
