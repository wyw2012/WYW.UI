using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace WYW.UI.Controls.Attach
{
    public class RangeElement
    {

        public static string GetStringFormat(DependencyObject obj) => (string)obj.GetValue(StringFormatProperty);

        public static void SetStringFormat(DependencyObject obj, string value) => obj.SetValue(StringFormatProperty, value);


        public static readonly DependencyProperty StringFormatProperty
            = DependencyProperty.RegisterAttached("StringFormat", typeof(string), typeof(RangeElement), new PropertyMetadata("N1"));

        #region ProgressBar 内部使用
        internal static bool GetIsAttached(DependencyObject obj) => (bool)obj.GetValue(IsAttachedProperty);

        internal static void SetIsAttached(DependencyObject obj, bool value) => obj.SetValue(IsAttachedProperty, value);


        internal static readonly DependencyProperty IsAttachedProperty
            = DependencyProperty.RegisterAttached("IsAttached", typeof(bool), typeof(RangeElement), new PropertyMetadata(default(bool), OnAttachedChanged));

        internal static double GetPercentage(DependencyObject obj) => (double)obj.GetValue(PercentageProperty);

        public static void SetPercentage(DependencyObject obj, double value) => obj.SetValue(PercentageProperty, value);

        /// <summary>
        /// 百分比，Value / (Maximum - Minimum)
        /// </summary>
        internal static readonly DependencyProperty PercentageProperty
            = DependencyProperty.RegisterAttached("Percentage", typeof(double), typeof(RangeElement), new PropertyMetadata(default(double)));

        internal static SolidColorBrush GetPercentageTextForeground(DependencyObject obj) => (SolidColorBrush)obj.GetValue(PercentageTextForegroundProperty);

        public static void SetPercentageTextForeground(DependencyObject obj, SolidColorBrush value) => obj.SetValue(PercentageTextForegroundProperty, value);

        /// <summary>
        /// 内部使用
        /// </summary>
        internal static readonly DependencyProperty PercentageTextForegroundProperty
            = DependencyProperty.RegisterAttached("PercentageTextForeground", typeof(SolidColorBrush), typeof(RangeElement), new PropertyMetadata(Application.Current.Resources["Foreground"] as SolidColorBrush));

        private static void OnAttachedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue && d is RangeBase bar)
            {
                SetPercentage(d, bar.Value / (bar.Maximum - bar.Minimum));
                // 如果行程超过一半，则字体颜色为白色
                if (bar.Value > (bar.Maximum - bar.Minimum) * 0.50)
                {
                    SetPercentageTextForeground(d, Application.Current.Resources["Item.ForegroundSelected"] as SolidColorBrush);
                }
                else
                {
                    SetPercentageTextForeground(d, Application.Current.Resources["Foreground"] as SolidColorBrush);
                }
                bar.ValueChanged += (a, b) =>
                {
                    SetPercentage(d, bar.Value / (bar.Maximum - bar.Minimum));
                    // 如果行程超过一半，则字体颜色为白色
                    if (bar.Value > (bar.Maximum - bar.Minimum) * 0.50)
                    {
                        SetPercentageTextForeground(d, Application.Current.Resources["Item.ForegroundSelected"] as SolidColorBrush);
                    }
                    else
                    {
                        SetPercentageTextForeground(d, Application.Current.Resources["Foreground"] as SolidColorBrush);
                    }
                };
            }
        }

        #endregion
    }
}
