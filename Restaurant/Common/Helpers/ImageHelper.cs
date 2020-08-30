using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Restaurant.Common.Helpers
{
    public static class ImageHelper
    {
        public static ImageSource ConvertFromByteArrayToImageSource(byte[] imageContent)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageContent);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            return biImg;
        }
    }
}
