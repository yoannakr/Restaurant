namespace Restaurant.Common.Helpers
{
    public static class BrowseFolderHelper
    {
        public static string BrowseFolder()
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files (*.png) | *.png"
            };

            bool? result = openFileDlg.ShowDialog();

            if (result == true)
            {
                return openFileDlg.FileName;
            }

            return null;
        }
    }
}
