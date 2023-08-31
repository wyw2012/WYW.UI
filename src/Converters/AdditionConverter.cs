using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace WYW.UI.Converters
{
    /// <summary>
    /// 加法转换器，返回value与parameter的之和，默认加1
    /// </summary>
    public class AdditionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var original = double.Parse(value.ToString());
                double para = 1;
                if(parameter != null)
                {
                    para = double.Parse(parameter.ToString());
                }
                return original+para;
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