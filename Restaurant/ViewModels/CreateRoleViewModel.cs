﻿using System;
using Prism.Commands;
using Restaurant.Views;
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
        private BaseViewModel baseViewModel;
        private CreateUserViewModel createUserViewModel;
        private string name;

        #endregion

        #region Constructors

        public CreateRoleViewModel()
        {
            roleService = new RoleService();
        }

        #endregion

        #region Properties

        public DelegateCommand<object> AddRoleCommand
        {
            get
            {
                if (addRoleCommand == null)
                    addRoleCommand = new DelegateCommand<object>(CreateRole,CanCreateRole);

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

        public BaseViewModel BaseViewModel
        {
            get => baseViewModel;
            set
            {
                baseViewModel = value;
                OnPropertyChanged("BaseViewModel");
            }
        }

        public CreateUserViewModel CreateUserViewModel
        {
            get
            {
                if (createUserViewModel == null)
                {
                    createUserViewModel = new CreateUserViewModel();
                    CreateUserView createUserView = new CreateUserView();

                    createUserViewModel.View = createUserView;

                    createUserView.DataContext = createUserViewModel;
                }

                return createUserViewModel;
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

        #endregion

        #region Methods

        private void CreateRole(object obj)
        {
            roleService.CreateRole(Name);
            BaseViewModel = CreateUserViewModel;
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
            BaseViewModel = CreateUserViewModel;
        }

        #endregion
    }
}
