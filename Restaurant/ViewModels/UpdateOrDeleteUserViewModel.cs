using System.Collections.ObjectModel;
using Restaurant.Common.InstanceHolder;

namespace Restaurant.ViewModels
{
    public class UpdateOrDeleteUserViewModel : BaseViewModel
    {
        #region Declarations

        private ObservableCollection<UserViewModel> users;

        #endregion

        #region Properties

        public ObservableCollection<UserViewModel> Users
        {
            get
            {
                if (users == null)
                    users = CollectionInstance.Instance.Users;

                return users;
            }
        }

        #endregion
    }
}
