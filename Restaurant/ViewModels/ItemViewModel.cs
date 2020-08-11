using System.IO;
using Prism.Commands;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Restaurant.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> addItemToSelected;

        #endregion

        #region Properties

        public string Name { get; set; }

        public decimal Price { get; set; }

        public byte[] Image { get; set; }

        public ImageSource ImageSource
        {
            get => ConvertFromByteArrayToImageSource();
        }

        public DelegateCommand<object> AddItemToSelected
        {
            get
            {
                if (addItemToSelected == null)
                    addItemToSelected = new DelegateCommand<object>(AddItem);

                return addItemToSelected;
            }
        }
 
        #endregion

        #region Methods

        private void AddItem(object obj)
        {
            MessageBox.Show($"{Name}");
        }

        private ImageSource ConvertFromByteArrayToImageSource()
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(Image);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            return biImg;
        }

        #endregion

    }
}
