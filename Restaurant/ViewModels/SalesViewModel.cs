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
                    allItemViewModel = new AllItemViewModel(this);
                    AllItemView allItemView = new AllItemView();

                    allItemViewModel.View = allItemView;

                    allItemView.DataContext = allItemViewModel;
                }

                return allItemViewModel;
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

                    selectedItemViewModel.View = selectedItemView;

                    selectedItemView.DataContext = selectedItemViewModel;
                }

                return selectedItemViewModel;
            }
        }

        #endregion
    }
}
