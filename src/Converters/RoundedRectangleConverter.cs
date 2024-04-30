using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WYW.UI.Converters
{
    /// <summary>
    /// 圆角矩形转换器
    /// </summary>
    public class RoundedRectangleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double height = double.Parse(value.ToString());
                return new CornerRadius(height / 2);
            }
            catch
            {
                return new CornerRadius();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
