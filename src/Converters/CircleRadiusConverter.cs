using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WYW.UI.Converters
{
    /// <summary>
    /// 根据控件的高度获取圆角需要的半径
    /// </summary>
    /// <remarks>返回value/2+parameter，例如value=40，parameter=2，则返回22</remarks>
    internal class CircleRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0;
            if (value != null)
            {
                double realValue = 0;
                if (double.TryParse(value.ToString(), out realValue))
                {
                    result = realValue / 2;
                }
            }
            if(parameter !=null)
            {
                double offset=0;
                if (double.TryParse(parameter.ToString(), out offset))
                {
                    result = result+offset;
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
