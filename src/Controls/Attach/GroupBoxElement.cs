using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WYW.UI.Controls.Attach
{
    public class GroupBoxElement
    {
        public static readonly DependencyProperty PlacementProperty
       = DependencyProperty.RegisterAttached("Placement", typeof(Dock), typeof(GroupBoxElement), new PropertyMetadata(Dock.Top));


        public static Dock GetPlacement(DependencyObject obj) => (Dock)obj.GetValue(PlacementProperty);

        public static void SetPlacement(DependencyObject obj, Dock value) => obj.SetValue(PlacementProperty, value);


   
    }
}
