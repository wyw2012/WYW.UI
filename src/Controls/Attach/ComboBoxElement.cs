using System.Windows;

namespace WYW.UI.Controls.Attach
{
    public class ComboBoxElement
    {

        public static readonly DependencyProperty TitleProperty
            = DependencyProperty.RegisterAttached("Title", typeof(string), typeof(ComboBoxElement), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty InputHintProperty
            = DependencyProperty.RegisterAttached("InputHint", typeof(string), typeof(ComboBoxElement), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty PopupHintProperty
            = DependencyProperty.RegisterAttached("PopupHint", typeof(string), typeof(ComboBoxElement), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty TitleWidthProperty
            = DependencyProperty.RegisterAttached("TitleWidth", typeof(string), typeof(ComboBoxElement), new PropertyMetadata("*"));
        public static readonly DependencyProperty SuffixProperty
            = DependencyProperty.RegisterAttached("Suffix", typeof(string), typeof(ComboBoxElement), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty SuffixWidthProperty
       = DependencyProperty.RegisterAttached("SuffixWidth", typeof(string), typeof(ComboBoxElement), new PropertyMetadata("0.5*"));

        public static string GetTitle(DependencyObject obj) => (string)obj.GetValue(TitleProperty);

        public static void SetTitle(DependencyObject obj, string value) => obj.SetValue(TitleProperty, value);

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

        public static CornerRadius GetCornerRadius(DependencyObject obj) => (CornerRadius)obj.GetValue(CornerRadiusProperty);

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value) => obj.SetValue(CornerRadiusProperty, value);


        public static readonly DependencyProperty CornerRadiusProperty
            = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ComboBoxElement), new PropertyMetadata(default(CornerRadius)));



        public static double GetComboBoxItemHeight(DependencyObject obj) => (double)obj.GetValue(ComboBoxItemHeightProperty);

        public static void SetComboBoxItemHeight(DependencyObject obj, double value) => obj.SetValue(ComboBoxItemHeightProperty, value);


        public static readonly DependencyProperty ComboBoxItemHeightProperty
            = DependencyProperty.RegisterAttached("ComboBoxItemHeight", typeof(double), typeof(ComboBoxElement), new PropertyMetadata(default(double)));


    }
}
