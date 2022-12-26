using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WYW.UI.Converters
{
    public class ConstToReverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (parameter == null)
                {
                    if(value is Boolean b)
                    {
                        return !b;
                    }
                    else
                    {
                        if (value.ToString() != "0")
                        {
                            return false;
                        }
                    }
                }
           
                else
                {
                    if (value.ToString() == parameter.ToString())
                    {
                        return false;
                    }
                }
            }
            return true;
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
