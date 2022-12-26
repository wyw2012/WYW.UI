using System.Windows;

namespace WYW.UI.Controls.Attach
{
    public class ToggleButtonElement
    {

        public static readonly DependencyProperty TitleProperty
            = DependencyProperty.RegisterAttached("Title", typeof(string), typeof(ToggleButtonElement), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty TitleWidthProperty
            = DependencyProperty.RegisterAttached("TitleWidth", typeof(string), typeof(ToggleButtonElement), new PropertyMetadata("*"));
        public static readonly DependencyProperty SuffixProperty
            = DependencyProperty.RegisterAttached("Suffix", typeof(string), typeof(ToggleButtonElement), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty SuffixWidthProperty
            = DependencyProperty.RegisterAttached("SuffixWidth", typeof(string), typeof(ToggleButtonElement), new PropertyMetadata("0.5*"));

        public static string GetTitle(DependencyObject obj) => (string)obj.GetValue(TitleProperty);

        public static void SetTitle(DependencyObject obj, string value) => obj.SetValue(TitleProperty, value);

        public static string GetTitleWidth(DependencyObject obj) => (string)obj.GetValue(TitleWidthProperty);

        public static void SetTitleWidth(DependencyObject obj, string value) => obj.SetValue(TitleWidthProperty, value);

        public static string GetSuffix(DependencyObject obj) => (string)obj.GetValue(SuffixProperty);

        public static void SetSuffix(DependencyObject obj, string value) => obj.SetValue(SuffixProperty, value);

        public static string GetSuffixWidth(DependencyObject obj) => (string)obj.GetValue(SuffixWidthProperty);

        public static void SetSuffixWidth(DependencyObject obj, string value) => obj.SetValue(SuffixWidthProperty, value);

    }
}
