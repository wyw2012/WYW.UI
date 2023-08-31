using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WYW.UI.Converters
{
    /// <summary>
    /// CornerRadius倍率转换器，参数必须是double[1]或者double[4]，数组之间用逗号隔离
    /// </summary>
    internal class CornerRadiusMultiplicationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return new CornerRadius();
            }
            if (value is CornerRadius cornerRadius)
            {
                if(parameter!=null)
                {
                    try
                    {
                        var array = parameter.ToString().Split(',').Select(x=>double.Parse(x)).ToArray();
                        if(array.Length>=4)
                        {
                            return new CornerRadius()
                            { 
                                TopLeft = cornerRadius.TopLeft * array[0],
                                TopRight = cornerRadius.TopRight * array[1],
                                BottomRight = cornerRadius.BottomRight * array[2],
                                BottomLeft = cornerRadius.BottomLeft * array[3],
                            };
                        }
                        else if (array.Length ==1)
                        {
                            return new CornerRadius()
                            {
                                TopLeft = cornerRadius.TopLeft * array[0],
                                TopRight = cornerRadius.TopRight * array[0],
                                BottomRight = cornerRadius.BottomRight * array[0],
                                BottomLeft = cornerRadius.BottomLeft * array[0],
                            };
                        }
                    }
                    catch
                    {
                 
                    }
                
                }
                return cornerRadius;
            }
            double ratio = 1;
            if (double.TryParse(value.ToString(), out ratio))
            {
                if (parameter != null)
                {
                    try
                    {
                        var array = parameter.ToString().Split(',').Select(x => double.Parse(x)).ToArray();
                        if (array.Length >= 4)
                        {
                            cornerRadius= new CornerRadius()
                            {
                                TopLeft = ratio * array[0],
                                TopRight = ratio * array[1],
                                BottomRight = ratio * array[2],
                                BottomLeft = ratio * array[3],
                            };
                            return cornerRadius;
                        }
                    }
                    catch
                    {

                    }
                }
            }
            return new CornerRadius();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
