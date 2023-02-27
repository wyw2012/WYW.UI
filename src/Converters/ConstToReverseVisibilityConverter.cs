using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WYW.UI.Converters
{

    /// <summary>
    /// 常量值与Visibility的逆向转换，例如：1=true=Visibility.Collapsed；0=false=Visibility.Visible；
    /// </summary>
    internal class ConstToReverseVisibilityConverter : IValueConverter
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