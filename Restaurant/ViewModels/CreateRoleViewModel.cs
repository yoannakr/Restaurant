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
        private IRoleService roleService;
        private BaseViewModel baseViewModel;
        private CreateUserViewModel createUserViewModel;

        #endregion

        #region Constructor

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
                    addRoleCommand = new DelegateCommand<object>(CreateRole);

                return addRoleCommand;
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

        public string Name { get; set; }

        #endregion

        #region Methods

        private void CreateRole(object obj)
        {
            roleService.CreateRole(Name);
            BaseViewModel = CreateUserViewModel;
        }

        #endregion
    }
}
