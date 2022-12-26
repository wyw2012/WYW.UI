using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WYW.UI.Converters
{
    /// <summary>
    /// 根据ProgressBar的值转换成对应的Thumb的CornerRadius
    /// </summary>
    internal class ProgressBarCornerRadiusConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            CornerRadius result = new CornerRadius();
            double width=0, height=0, currentValue = 0,maximum = 100;
            Orientation orientation= Orientation.Horizontal;
            if (values != null && values.Length >=5)
            {
                width = double.Parse(values[0].ToString());
                height = double.Parse(values[1].ToString());
                currentValue = double.Parse(values[2].ToString());
                maximum = double.Parse(values[3].ToString());
                orientation = (Orientation)Enum.Parse(typeof(Orientation), values[4].ToString());
            }
            if(orientation== Orientation.Horizontal)
            {
                if (currentValue / maximum > (width - height / 2) / width)
                {
                    result = new CornerRadius(height / 2);
                }
                else
                {
                    result = new CornerRadius(height / 2, 0, 0, height / 2);
                }
            }
            else
            {
                if (currentValue / maximum > (width - height / 2) / width)
                {
                    result = new CornerRadius(height / 2);
                }
                else
                {
                    result = new CornerRadius(height / 2, 0, 0, height / 2);
                }
            }
           
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
