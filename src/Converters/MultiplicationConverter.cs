using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace WYW.UI.Converters
{
    /// <summary>
    /// 乘法转换器，返回value与parameter的乘积
    /// </summary>
    public class MultiplicationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var original = double.Parse(value.ToString());
                var para = double.Parse(parameter.ToString());

                if(para==0.1)
                {
                    int a =0;
                }
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