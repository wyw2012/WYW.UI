using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WYW.UI.Converters
{

    /// <summary>
    /// 常量值与Visibility的反向映射，如果绑定值与参数相反，则返回Visibility.Visible
    /// </summary>
    public class ConstToReverseVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (parameter == null)
                {
                    if (value is int)
                    {
                        if (value.ToString().ToLower() != "0")
                        {
                            return Visibility.Collapsed;
                        }
                    }
                    else if (value is bool)
                    {
                        if (value.ToString().ToLower() == "true")
                        {
                            return Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        return Visibility.Collapsed;
                    }
                }
                else
                {
                    if (value.ToString() == parameter.ToString())
                    {
                        return Visibility.Collapsed;
                    }
                }
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}