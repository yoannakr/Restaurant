using Prism.Commands;
using Restaurant.Views;

namespace Restaurant.ViewModels
{
    public class AdminPanelViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> createCommand;
        private CreateUserViewModel createUserViewModel;
        private CreateItemViewModel createItemViewModel;

        #endregion

        #region Constructors

        public AdminPanelViewModel(MenuViewModel menuViewModel)
        {
            MenuViewModel = menuViewModel;
        }

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

        public MenuViewModel MenuViewModel { get; set; }

        public CreateUserViewModel CreateUserViewModel
        {
            get
            {
                createUserViewModel = new CreateUserViewModel(MenuViewModel);
                CreateUserView createUserView = new CreateUserView();

                createUserViewModel.View = createUserView;

                createUserView.DataContext = createUserViewModel;

                return createUserViewModel;
            }
        }

        public CreateItemViewModel CreateItemViewModel
        {
            get
            {
                createItemViewModel = new CreateItemViewModel(MenuViewModel);
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
                MenuViewModel.BaseViewModel = baseViewModel;
        }

        #endregion
    }
}
