using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WYW.UI.Converters
{
    /// <summary>
    /// 常量值与布尔值的正向转换，例如：1=true；0=false
    /// </summary>
    internal class ConstToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (parameter == null)
                {
                    if (value.ToString() != "0")
                    {
                        return true;
                    }
                }
                else
                {
                    if (value.ToString() == parameter.ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                if (value is Boolean temp && temp)
                {
                    return int.Parse(parameter.ToString());
                }
            }
            return 0;
        }
    }
}
