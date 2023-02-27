using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WYW.UI.Converters
{
    /// <summary>
    /// 常量值与Visibility的正向向转换，例如：1=true=Visibility.Visible；0=false=Visibility.Collapsed；
    /// </summary>
    public class ConstToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!=null && !string.IsNullOrEmpty(value.ToString()))
            {
                if (parameter == null)
                {
                    if(value is int)
                    {
                        if (value.ToString().ToLower() != "0")
                        {
                            return Visibility.Visible;
                        }
                    }
                    else if(value is bool)
                    {
                        if (value.ToString().ToLower() == "true" )
                        {
                            return Visibility.Visible;
                        }
                    }
                    else
                    {
                        return Visibility.Visible;
                    }
                }
                else
                {
                    if (value.ToString() == parameter.ToString())
                    {
                        return Visibility.Visible;
                    }
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}