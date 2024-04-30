using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WYW.UI.Converters
{
    public class EnumToInt32Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = value.GetType();
            var array = type.GetEnumValues();
            return Array.IndexOf(array, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var obj = Enum.ToObject(targetType, value);
            return obj;
        }
    }
}
