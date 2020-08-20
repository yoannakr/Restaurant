using System.Windows;
using System.Windows.Controls;

namespace Restaurant.Common.Behaviors
{
    public static class PasswordHelper
    {
        #region Declarations

        public static readonly DependencyProperty PasswordProperty =
           DependencyProperty.RegisterAttached("Password",
           typeof(string), typeof(PasswordHelper));

        public static readonly DependencyProperty AttachProperty =
           DependencyProperty.RegisterAttached("Attach",
           typeof(bool), typeof(PasswordHelper),
           new PropertyMetadata(false, Attach));

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

        public static bool GetAttach(DependencyObject dp)
        {
            return (bool)dp.GetValue(AttachProperty);
        }

        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }

        private static void Attach(DependencyObject dp,
            DependencyPropertyChangedEventArgs e)
        {
            if (!(dp is PasswordBox passwordBox))
                return;

            if ((bool)e.OldValue)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
            }

            if ((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
                SetPassword(passwordBox, passwordBox.Password);
        }

        #endregion
    }
}
