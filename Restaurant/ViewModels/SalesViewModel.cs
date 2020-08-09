using Restaurant.Views;

namespace Restaurant.ViewModels
{
    public class SalesViewModel : BaseViewModel
    {
        #region Declarations

        private AllItemViewModel allItemViewModel;
        private SelectedItemViewModel selectedItemViewModel;

        #endregion

        #region Properties

        public AllItemViewModel AllItemViewModel
        {
            get
            {
                if (allItemViewModel == null)
                {
                    allItemViewModel = new AllItemViewModel();
                    AllItemView allItemView = new AllItemView();

                    allItemViewModel.View = allItemView;

                    allItemView.DataContext = allItemViewModel;
                }

                return allItemViewModel;
            }
            set
            {
                allItemViewModel = value;
                OnPropertyChanged("AllItemViewModel");
            }
        }

        public SelectedItemViewModel SelectedItemViewModel
        {
            get
            {
                if (selectedItemViewModel == null)
                {
                    selectedItemViewModel = new SelectedItemViewModel();
                    SelectedItemView selectedItemView = new SelectedItemView();

                    allItemViewModel.View = selectedItemView;

                    selectedItemView.DataContext = allItemViewModel;
                }

                return selectedItemViewModel;
            }
            set
            {
                selectedItemViewModel = value;
                OnPropertyChanged("SelectedItemViewModel");
            }
        }

        #endregion
    }
}
