using System.Windows;
using System.Windows.Controls;

namespace Restaurant.Behaviors
{
    public static class PasswordHelper
    {
        #region Declarations

        public static readonly DependencyProperty PasswordProperty =
           DependencyProperty.RegisterAttached("Password",
           typeof(string), typeof(PasswordHelper),
           new PropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        #endregion

        #region Methods

        public static string GetPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject dp, string value)
        {
            dp.SetValue(PasswordProperty, value);
        }

        private static void OnPasswordPropertyChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;

            passwordBox.PasswordChanged += PasswordChanged;
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            SetPassword(passwordBox, passwordBox.Password);
        }

        #endregion
    }
}
