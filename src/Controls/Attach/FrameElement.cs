using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WYW.UI.Controls.Attach
{
    public class FrameElement
    {
        public static readonly DependencyProperty CornerRadiusProperty
          = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(FrameElement), new PropertyMetadata(default(CornerRadius)));

        public static CornerRadius GetCornerRadius(DependencyObject obj) => (CornerRadius)obj.GetValue(CornerRadiusProperty);

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value) => obj.SetValue(CornerRadiusProperty, value);


      

    }
}
