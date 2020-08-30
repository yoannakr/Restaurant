using System.Collections.ObjectModel;
using Restaurant.Common.InstanceHolder;

namespace Restaurant.ViewModels
{
    public class UpdateOrDeleteTableViewModel : BaseViewModel
    {
        #region Declarations

        private ObservableCollection<TableViewModel> tables;

        #endregion

        #region Properties

        public ObservableCollection<TableViewModel> Tables
        {
            get
            {
                if (tables == null)
                    tables = CollectionInstance.Instance.Tables;

                return tables;
            }
        }

        #endregion
    }
}
