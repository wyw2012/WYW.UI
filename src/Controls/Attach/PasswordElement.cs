using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace WYW.UI.Controls.Attach
{
    public class PasswordElement
    {

        static PasswordElement()
        {

        }
        public static readonly DependencyProperty TitleProperty
            = DependencyProperty.RegisterAttached("Title", typeof(string), typeof(PasswordElement), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty IsNecessaryProperty
            = DependencyProperty.RegisterAttached("IsNecessary", typeof(bool), typeof(PasswordElement), new PropertyMetadata(default(bool)));
        public static readonly DependencyProperty InputHintProperty
            = DependencyProperty.RegisterAttached("InputHint", typeof(string), typeof(PasswordElement), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty PopupHintProperty
            = DependencyProperty.RegisterAttached("PopupHint", typeof(string), typeof(PasswordElement), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty TitleWidthProperty
            = DependencyProperty.RegisterAttached("TitleWidth", typeof(string), typeof(PasswordElement), new PropertyMetadata("*"));
        public static readonly DependencyProperty SuffixProperty
            = DependencyProperty.RegisterAttached("Suffix", typeof(string), typeof(PasswordElement), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty SuffixWidthProperty
            = DependencyProperty.RegisterAttached("SuffixWidth", typeof(string), typeof(PasswordElement), new PropertyMetadata("0.5*"));
        public static readonly DependencyProperty IsCircleCornerProperty
            = DependencyProperty.RegisterAttached("IsCircleCorner", typeof(bool), typeof(PasswordElement), new PropertyMetadata(default(bool)));
        public static readonly DependencyProperty CornerRadiusProperty
            = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(PasswordElement), new PropertyMetadata(default(CornerRadius)));

        public static string GetTitle(DependencyObject obj) => (string)obj.GetValue(TitleProperty);

        public static void SetTitle(DependencyObject obj, string value) => obj.SetValue(TitleProperty, value);

        public static bool GetIsNecessary(DependencyObject obj) => (bool)obj.GetValue(IsNecessaryProperty);

        public static void SetIsNecessary(DependencyObject obj, bool value) => obj.SetValue(IsNecessaryProperty, value);

        public static string GetInputHint(DependencyObject obj) => (string)obj.GetValue(InputHintProperty);

        public static void SetInputHint(DependencyObject obj, string value) => obj.SetValue(InputHintProperty, value);

        public static string GetPopupHint(DependencyObject obj) => (string)obj.GetValue(PopupHintProperty);

        public static void SetPopupHint(DependencyObject obj, string value) => obj.SetValue(PopupHintProperty, value);

        public static string GetTitleWidth(DependencyObject obj) => (string)obj.GetValue(TitleWidthProperty);

        public static void SetTitleWidth(DependencyObject obj, string value) => obj.SetValue(TitleWidthProperty, value);

        public static string GetSuffix(DependencyObject obj) => (string)obj.GetValue(SuffixProperty);

        public static void SetSuffix(DependencyObject obj, string value) => obj.SetValue(SuffixProperty, value);

        public static string GetSuffixWidth(DependencyObject obj) => (string)obj.GetValue(SuffixWidthProperty);

        public static void SetSuffixWidth(DependencyObject obj, string value) => obj.SetValue(SuffixWidthProperty, value);
        public static bool GetIsCircleCorner(DependencyObject obj) => (bool)obj.GetValue(IsCircleCornerProperty);

        public static void SetIsCircleCorner(DependencyObject obj, bool value) => obj.SetValue(IsCircleCornerProperty, value);
        public static CornerRadius GetCornerRadius(DependencyObject obj) => (CornerRadius)obj.GetValue(CornerRadiusProperty);

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value) => obj.SetValue(CornerRadiusProperty, value);

        public static string GetPassword(DependencyObject obj) => (string)obj.GetValue(PasswordProperty);

        public static void SetPassword(DependencyObject obj, string value) => obj.SetValue(PasswordProperty, value);


        public static readonly DependencyProperty PasswordProperty
            = DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordElement), new PropertyMetadata(null, PasswordChanged));

        private static void PasswordChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

            if (sender is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
                passwordBox.Password = e.NewValue.ToString();
                passwordBox.PasswordChanged += PasswordChanged;
                // 光标移动到最后
                passwordBox.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic) .Invoke(passwordBox, new object[] { passwordBox.Password.Length, 0 });
            
            }
        }
        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                passwordBox.SetValue(PasswordProperty, passwordBox.Password);
            }
        }
    }
}
