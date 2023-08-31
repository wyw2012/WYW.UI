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
    /// 大于等于某个数值时则为True
    /// </summary>
    internal class MoreThanToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter!=null)
            {
                try
                {
                    double value1 = double.Parse(value.ToString());
                    double value2 = double.Parse(parameter.ToString());
                    if (value1 >= value2)
                    {
                        return true;
                    }
                }
                catch
                { 
                }
                
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
