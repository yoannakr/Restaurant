﻿using System.Linq;
using Prism.Commands;
using Restaurant.Views;
using Restaurant.Database.Models;
using Restaurant.Services.Models.User;

namespace Restaurant.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> changeMenuViewCommand;
        private DelegateCommand exitCommand;
        private BaseViewModel baseViewModel;
        private AllTablesViewModel alltablesViewModel;
        private AdminPanelViewModel adminPanelViewModel;
        private LoginViewModel loginViewModel;
        private UserDto userDto;
        private static MenuViewModel instance = null;
        private bool adminBtnVisibility;

        #endregion

        #region Constructors

        private MenuViewModel()
        {
            BaseViewModel = AllTablesViewModel;
        }

        #endregion

        #region Properties

        public DelegateCommand<object> ChangeMenuViewCommand
        {
            get
            {
                if (changeMenuViewCommand == null)
                    changeMenuViewCommand = new DelegateCommand<object>(ChangeMenuView);

                return changeMenuViewCommand;
            }
        }

        public DelegateCommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                    exitCommand = new DelegateCommand(Exit);

                return exitCommand;
            }
        }

        public static MenuViewModel Instance
        {
            get
            {
                if (instance == null)
                    instance = new MenuViewModel();

                return instance;
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

        public AllTablesViewModel AllTablesViewModel
        {
            get
            {
                if (alltablesViewModel == null)
                {
                    alltablesViewModel = new AllTablesViewModel();
                    AllTablesView alltablesView = new AllTablesView();

                    alltablesViewModel.View = alltablesView;

                    alltablesView.DataContext = alltablesViewModel;
                }

                return alltablesViewModel;
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

        public LoginViewModel LoginViewModel
        {
            get
            {
                loginViewModel = new LoginViewModel();
                LoginView loginView = new LoginView();

                loginViewModel.View = loginView;

                loginView.DataContext = loginViewModel;

                return loginViewModel;
            }
        }

        public UserDto UserDto
        {
            get => userDto;
            set
            {
                userDto = value;

                UserRole role = userDto.Roles
                                       .Where(u => u.Role.Name == "Админ")
                                       .FirstOrDefault();

                if (role != null)
                    AdminBtnVisibility = true;
                else
                    AdminBtnVisibility = false;
            }
        }

        public bool AdminBtnVisibility
        {
            get => adminBtnVisibility;
            set
            {
                adminBtnVisibility = value;
                OnPropertyChanged("AdminBtnVisibility");
            }
        }

        #endregion

        #region Methods

        private void ChangeMenuView(object obj)
        {
            if (obj is BaseViewModel baseViewModel)
                BaseViewModel = baseViewModel;
        }

        private void Exit()
        {
            MainWindowViewModel.Instance.ChangeMainViewCommand.Execute(LoginViewModel);
        }

        #endregion
    }
}
