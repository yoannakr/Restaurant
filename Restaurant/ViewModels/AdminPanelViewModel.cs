using Prism.Commands;
using Restaurant.Views;

namespace Restaurant.ViewModels
{
    public class AdminPanelViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> changeAdminViewCommand;
        private CreateUserViewModel createUserViewModel;
        private UpdateOrDeleteUserViewModel updateOrDeleteUserViewModel;
        private CreateTableViewModel createTableViewModel;
        private CreateItemViewModel createItemViewModel;

        #endregion

        #region Properties

        public DelegateCommand<object> ChangeAdminViewCommand
        {
            get
            {
                if (changeAdminViewCommand == null)
                    changeAdminViewCommand = new DelegateCommand<object>(ChangeAdminView);

                return changeAdminViewCommand;
            }
        }

        public CreateUserViewModel CreateUserViewModel
        {
            get
            {
                createUserViewModel = new CreateUserViewModel();
                CreateUserView createUserView = new CreateUserView();

                createUserViewModel.View = createUserView;

                createUserView.DataContext = createUserViewModel;

                return createUserViewModel;
            }
        }

        public UpdateOrDeleteUserViewModel UpdateOrDeleteUserViewModel
        {
            get
            {
                updateOrDeleteUserViewModel = new UpdateOrDeleteUserViewModel();
                UpdateOrDeleteUserView updateOrDeleteUserView = new UpdateOrDeleteUserView();

                updateOrDeleteUserViewModel.View = updateOrDeleteUserView;

                updateOrDeleteUserView.DataContext = updateOrDeleteUserViewModel;

                return updateOrDeleteUserViewModel;
            }
        }

        public CreateTableViewModel CreateTableViewModel
        {
            get
            {
                createTableViewModel = new CreateTableViewModel();
                CreateTableView createTableView = new CreateTableView();

                createTableViewModel.View = createTableView;

                createTableView.DataContext = createTableViewModel;

                return createTableViewModel;
            }
        }

        public CreateItemViewModel CreateItemViewModel
        {
            get
            {
                createItemViewModel = new CreateItemViewModel();
                CreateItemView createItemView = new CreateItemView();

                createItemViewModel.View = createItemView;

                createItemView.DataContext = createItemViewModel;

                return createItemViewModel;
            }
        }

        #endregion

        #region Methods

        private void ChangeAdminView(object obj)
        {
            if (obj is BaseViewModel baseViewModel)
                MenuViewModel.Instance.ChangeMenuViewCommand.Execute(baseViewModel);
        }

        #endregion
    }
}
