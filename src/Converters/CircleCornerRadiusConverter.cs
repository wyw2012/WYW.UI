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
    /// 根据控件的高度获取圆角需要的CornerRadius
    /// </summary>
    /// <remarks>例如：如果value=40，则返回CornerRadius(20,20,20,20)</remarks>
    internal class CircleCornerRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CornerRadius result = new CornerRadius();
            if (value != null)
            {
                double height = 0;
                if (double.TryParse(value.ToString(), out height))
                {
                    result = new CornerRadius(height / 2);
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
