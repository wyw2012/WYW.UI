using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace WYW.UI.Converters
{
    /// <summary>
    /// 乘法转换器，返回绑定值与参数值的乘积
    /// </summary>
    public class MultiplicationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var original = double.Parse(value.ToString());
                var para = double.Parse(parameter.ToString());
                return original*para;
            }
            catch
            {
                return -1;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}