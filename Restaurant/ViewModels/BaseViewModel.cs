using System.ComponentModel;
using Restaurant.Interfaces;

namespace Restaurant.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Declarations

        private IView view;

        #endregion

        #region Properties

        public IView View
        {
            get => this.view;
            set
            {
                if (value == view)
                    return;

                view = value;

                view.DataContext = this;
                OnPropertyChanged("View");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion       
    }
}
