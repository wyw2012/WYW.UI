using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace WYW.UI.Converters
{
    /// <summary>
    /// 枚举描述转换器
    /// </summary>
    public class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum newValue)
            {
                var field = newValue.GetType().GetField(newValue.ToString());
                var customAttribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                return customAttribute == null ? newValue.ToString() : ((DescriptionAttribute)customAttribute).Description;
            }
            return value?.ToString() ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}