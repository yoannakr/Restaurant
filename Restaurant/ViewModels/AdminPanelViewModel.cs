using Prism.Commands;
using Restaurant.Views;
using System.Windows.Input;

namespace Restaurant.ViewModels
{
    public class AdminPanelViewModel : BaseViewModel
    {
        #region Declarations

        private ICommand newUserCommand;
        private BaseViewModel baseViewModel;
        private CreateUserViewModel createUserViewModel;

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

        #endregion

        #region Methods

        private void CreateUser(object obj)
        {
            BaseViewModel = CreateUserViewModel;
        }

        #endregion
    }
}
