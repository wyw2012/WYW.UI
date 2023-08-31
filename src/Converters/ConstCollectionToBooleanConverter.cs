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
    /// 常量值集合与布尔值的正向转换，在集合内为true，集合通过空格隔离
    /// </summary>
    internal class ConstCollectionToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null)
            {
                try
                {
                    string value1 = value.ToString();
                    var stringArray = parameter.ToString().Split(' ').Where(x => x != "").ToArray();
                    if (stringArray.Any(x => x == value1))
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
