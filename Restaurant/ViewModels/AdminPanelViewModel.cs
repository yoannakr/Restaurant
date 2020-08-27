using Prism.Commands;
using Restaurant.Views;

namespace Restaurant.ViewModels
{
    public class AdminPanelViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> createCommand;
        private CreateUserViewModel createUserViewModel;
        private CreateTableViewModel createTableViewModel;
        private CreateItemViewModel createItemViewModel;

        #endregion

        #region Properties

        public DelegateCommand<object> CreateCommand
        {
            get
            {
                if (createCommand == null)
                    createCommand = new DelegateCommand<object>(Create);

                return createCommand;
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

        private void Create(object obj)
        {
            if (obj is BaseViewModel baseViewModel)
                MenuViewModel.Instance.ChangeMenuViewCommand.Execute(baseViewModel);
        }

        #endregion
    }
}
