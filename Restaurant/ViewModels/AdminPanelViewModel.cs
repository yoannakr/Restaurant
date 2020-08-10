using Prism.Commands;
using Restaurant.Views;
using System.Windows.Input;

namespace Restaurant.ViewModels
{
    public class AdminPanelViewModel : BaseViewModel
    {
        #region Declarations

        private ICommand newUserCommand;
        private ICommand newItemCommand;
        private BaseViewModel baseViewModel;
        private CreateUserViewModel createUserViewModel;
        private CreateItemViewModel createItemViewModel;

        #endregion

        #region Properties

        public ICommand NewUserCommand 
        {
            get
            {
                if(newUserCommand == null)
                    newUserCommand = new DelegateCommand<object>(CreateUser);

                return newUserCommand;
            }
        }

        public ICommand NewItemCommand 
        { get
            {
                if (newItemCommand == null)
                    newItemCommand = new DelegateCommand<object>(CreateItem);

                return newItemCommand;
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
            set
            {
                createUserViewModel = value;
                OnPropertyChanged("CreateUserViewModel");
            }
        }

        public CreateItemViewModel CreateItemViewModel 
        {
            get
            {
                if (createItemViewModel == null)
                {
                    createItemViewModel = new CreateItemViewModel();
                    CreateItemView createItemView = new CreateItemView();

                    createItemViewModel.View = createItemView;

                    createItemView.DataContext = createItemViewModel;
                }

                return createItemViewModel;
            }
            set
            {
                createItemViewModel = value;
                OnPropertyChanged("CreateItemViewModel");
            }
        }

        #endregion

        #region Methods

        private void CreateUser(object obj)
        {
            BaseViewModel = CreateUserViewModel;
        }

        private void CreateItem(object obj)
        {
            BaseViewModel = CreateItemViewModel;
        }

        #endregion
    }
}
