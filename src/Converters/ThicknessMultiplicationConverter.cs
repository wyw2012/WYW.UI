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
    /// Thickness倍率转换器，参数必须是double[1]或者double[4]，数组之间用逗号隔离
    /// </summary>
    internal class ThicknessMultiplicationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return new Thickness();
            }
            if (value is Thickness thickness)
            {
                if(parameter!=null)
                {
                    try
                    {
                        var array = parameter.ToString().Split(',').Select(x=>double.Parse(x)).ToArray();
                        if(array.Length>=4)
                        {
                            return new Thickness()
                            {
                                Left = thickness.Left * array[0],
                                Top = thickness.Top * array[1],
                                Right = thickness.Right * array[2],
                                Bottom = thickness.Bottom * array[3],
                            };
                        }
                        else if (array.Length ==1)
                        {
                            return new Thickness()
                            {
                                Left = thickness.Left * array[0],
                                Top = thickness.Top * array[0],
                                Right = thickness.Right * array[0],
                                Bottom = thickness.Bottom * array[0],
                            };
                        }
                    }
                    catch
                    {
                 
                    }
                
                }
                return thickness;
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
                            thickness= new Thickness()
                            {
                                Left = ratio * array[0],
                                Top = ratio * array[1],
                                Right = ratio * array[2],
                                Bottom = ratio * array[3],
                            };
                            return thickness;
                        }
                    }
                    catch
                    {

                    }
                }
            }
            return new Thickness();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
