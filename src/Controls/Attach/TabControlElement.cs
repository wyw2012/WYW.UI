using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WYW.UI.Controls.Attach
{
    public class TabControlElement
    {
        public static readonly DependencyProperty ShowCloseButtonProperty
            = DependencyProperty.RegisterAttached("ShowCloseButton", typeof(bool), typeof(TabControlElement), new PropertyMetadata(default(bool)));
   
        public static bool GetShowCloseButton(DependencyObject obj) => (bool)obj.GetValue(ShowCloseButtonProperty);

        public static void SetShowCloseButton(DependencyObject obj, bool value) => obj.SetValue(ShowCloseButtonProperty, value);


    }
}
